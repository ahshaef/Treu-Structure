using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model
{
    [Serializable]
    public class AreaProps : ICloneable
    {
        private Section.AreaSection section = null;

        /// <summary>
        /// La �nica propiedad de esta clase AreaProps es la secci�n del �rea.
        /// Si la secci�n no est� en el cat�logo del modelo, se agrega.
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        //[System.ComponentModel.Editor(typeof(Canguro.Controller.PropertyGrid.AreaSectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Section.AreaSection Section
        {
            get
            {
                if (section == null)
                    section = Canguro.Model.Section.SectionManager.Instance.DefaultAreaSection as Section.AreaSection;
                return section;
            }
            set
            {
                if (value != null)
                {
                    if (section != null)
                        Model.Instance.Undo.Change(this, section, GetType().GetProperty("Section"));
                    // Si no est� con el mismo nombre, se agrega
                    if (Model.Instance.Sections[value.Name] == null)
                        Model.Instance.Sections[value.Name] = value;
                    // Si hay otra secci�n con el mismo nombre, se cambia el nombre y se agrega
                    if (Model.Instance.Sections[value.Name] != value)
                    {
                        int i = 1;
                        string name = value.Name;
                        value.Name = name + "(" + i++ + ")";
                        while (Model.Instance.Sections[value.Name] != null)
                            value.Name = name + "(" + i++ + ")";
                        Model.Instance.Sections[value.Name] = value;
                    }
                    // Se asigna
                    section = value;
                    Canguro.Model.Section.SectionManager.Instance.DefaultAreaSection = section;
                }
            }
        }

        /// <summary>
        /// M�todo heredado de IClonable
        /// </summary>
        /// <returns>Regresa una copia superficial (completa en este caso) de s� mismo</returns>
        public object Clone()
        {
            AreaProps clone = (AreaProps)this.MemberwiseClone();
            if (clone.section != null)
            {
                if (Model.Instance.Sections[clone.section.Name] == null)
                    Model.Instance.Sections[clone.section.Name] = clone.section;
                else if (Model.Instance.Sections[clone.section.Name] is Canguro.Model.Section.AreaSection)
                    clone.section = (Canguro.Model.Section.AreaSection)Model.Instance.Sections[clone.section.Name];
            }
            return clone;
        }
    }
}
