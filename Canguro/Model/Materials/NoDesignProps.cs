using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Material
{
    /// <summary>
    /// Esta clase define materiales sin propiedades espec�ficas de dise�o (materiales generals).
    /// Se usa cuando se requiere de an�lisis pero no de dise�o.
    /// </summary>
    [Serializable]
    public class NoDesignProps : MaterialDesignProps
    {
        /// <summary>
        /// La �nica propiedad de esta clase es el nombre, que se define para cada cultura.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public override string Name
        {
            get
            {
                return Culture.Get("noMaterialName");
            }
        }
    }
}
