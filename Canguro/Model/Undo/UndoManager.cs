using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Canguro.Model.Undo
{
    /// <summary>
    /// Clase que administra la historia de cambios al modelo.
    /// Todas las propiedades le tienen que avisar sobre los cambios por dos motivos:
    /// para poder hacer Undo y para revisar que el modelo no est� bloqueado.
    /// Si el modelo est� bloqueado, se lanza una ModelIsLockedException
    /// </summary>
    public class UndoManager
    {
        private LinkedList<ActionList> actionLists;
        private LinkedListNode<ActionList> undoPtr;
        private ActionList currentAction;
        private bool undoing;
        private IModel model;

        /// <summary>
        /// Constructora que inicializa la lista de acciones como vac�a.
        /// </summary>
        public UndoManager(IModel model)
        {
            this.model = model;
            actionLists = new LinkedList<ActionList>();
            currentAction = new ActionList();
            undoPtr = actionLists.Last;
            undoing = false;
        }

        public bool Enabled
        {
            get { return !undoing; }
            set { undoing = !value; }
        }

        public bool CanUndo
        {
            get
            {
                return undoPtr != null;
            }
        }

        public bool CanRedo
        {
            get
            {
                return undoPtr != actionLists.Last;
            }
        }
    
        /// <summary>
        /// Funci�n que se llama desde las propiedades cada que hay un cambio.
        /// </summary>
        /// <param name="obj">El objeto que cambi�</param>
        /// <param name="oldValue">El valor anterior</param>
        /// <param name="property">La propiedad que cambi�</param>
        public void Change(object obj, object oldValue, System.Reflection.PropertyInfo property)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new ChangeAction(obj, oldValue, property));
            }

        }

        /// <summary>
        /// Method called when an object is added to a Catalog
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="itemList"></param>
        public void Change<Ttype>(Ttype oldValue, string key, Catalog<Ttype> catalog)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new ChangeCatalogAction<Ttype>(oldValue, key, catalog));
            }
        }

        /// <summary>
        /// Funci�n que se llama cuando se agrega un Item a un ItemList
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="itemList"></param>
        public void Add(object obj, System.Collections.IList itemList)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new AddDelAction(obj, itemList, true));
            }
        }

        /// <summary>
        /// Funci�n que se llama cuando se agrega un ItemList a un Dictionary.
        /// Se usa en AssignedLoads.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="dictionary"></param>
        public void Add(object key, object obj, System.Collections.ICollection collection)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new AddDelListAction(key, obj, collection, true));
            }
        }

        /// <summary>
        /// Agrega una acci�n de borrado.
        /// </summary>
        /// <param name="obj">El objeto que se elimin�.</param>
        public void Remove(object obj, System.Collections.IList itemList)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new AddDelAction(obj, itemList, false));
            }
        }

        /// <summary>
        /// Se llama cuando se elimina una ItemList de un Dictionary.
        /// Se usa en AssignedLoads
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="collection"></param>
        public void Remove(object key, object obj, System.Collections.ICollection collection)
        {
            if (!undoing)
            {
                model.Modified = true;
                currentAction.Actions.Add(new AddDelListAction(key, obj, collection, false));
            }
        }

        /// <summary>
        /// Agrega la acci�n actual a la lista de historia, mueve el apuntador, 
        /// elimina lo que queda de la lista y crea una nueva acci�n actual.
        /// Avisa al modelo que hubo cambios.
        /// </summary>
        public void Commit()
        {
            if (currentAction.Actions.Count > 0)
            {
                while (actionLists.Last != null && actionLists.Last != undoPtr)
                    actionLists.RemoveLast();
                actionLists.AddLast(currentAction);
                undoPtr = actionLists.Last;
                currentAction = new ActionList();
                RemoveTrailing();
                model.ChangeModel();
            }
        }

        private void RemoveTrailing()
        {
            int count = 0;
            foreach (ActionList list in actionLists)
                count += list.Actions.Count;
            int max = Canguro.Properties.Settings.Default.UndoLimit;
            while (count > max)
            {
                count -= actionLists.First.Value.Actions.Count;
                actionLists.RemoveFirst();
            }
        }

        /// <summary>
        /// Deshace la acci�n actual 
        /// </summary>
        public void Rollback()
        {
            currentAction.Undo();
            currentAction = new ActionList();
        }

        /// <summary>
        /// Deshace la �ltima acci�n.
        /// </summary>
        public void Undo()
        {
            if (undoPtr != null)
            {
                model.Modified = true;
                undoing = true;
                Rollback();
                currentAction = undoPtr.Value;
                currentAction.Undo();
                undoPtr = undoPtr.Previous;
                currentAction = new ActionList();
                undoing = false;
            }
        }

        /// <summary>
        /// Deshace un Undo. Hace undo de la siguiente acci�n (debe estar invertida) y mueve 
        /// undoPtr a la siguiente posici�n en la lista.
        /// </summary>
        public void Redo()
        {
            if (undoPtr != actionLists.Last)
            {
                model.Modified = true;
                undoing = true;
                Rollback();
                if (undoPtr == null)
                    undoPtr = actionLists.First;
                else
                    undoPtr = undoPtr.Next;
                undoPtr.Value.Redo();
                undoing = false;
            }
        }
    }
}
