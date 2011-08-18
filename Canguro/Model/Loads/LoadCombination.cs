using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Load
{
    /// <summary>
    /// Clase que representa una combinaci�n (Combo) de casos de an�lisis abstractos. 
    /// As�, esta clase forma un Composite.
    /// </summary>
    [Serializable]
    public class LoadCombination : AbstractCase
    {
        private CombinationType combinationType = CombinationType.LinearAdd;
        private List<AbstractCaseFactor> cases = new List<AbstractCaseFactor>();

        /// <summary>
        /// Constructora que asigna el nombre.
        /// </summary>
        /// <param name="name"></param>
        public LoadCombination(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Enumeraci�n de los tipos de combinaci�n admitidos.
        /// </summary>
        public enum CombinationType
        {
            LinearAdd, Envelope, AbsoluteAdd, SRSS,
        }

        /// <summary>
        /// Lista de AbstractCases y respectivos factores.
        /// Es de s�lo lectura, por lo que no se incluye en la funci�n Undo.
        /// </summary>
        public List<AbstractCaseFactor> Cases
        {
            get
            {
                return cases;
            }
        }

        /// <summary>
        /// Tipo de combinaci�n. Especifica la forma en que se combinar�n los estados de an�lisis.
        /// </summary>
        public CombinationType Type
        {
            get
            {
                return combinationType;
            }
            set
            {
                if (value != combinationType)
                {
                    Model.Instance.Undo.Change(this, combinationType, GetType().GetProperty("Type"));
                    combinationType = value;
                }
            }
        }
    }
}
