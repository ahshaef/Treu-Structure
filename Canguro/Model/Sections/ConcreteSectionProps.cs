using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Section
{
    /// <summary>
    /// Abstracci�n de las propiedades de concreto. Engloba las propiedades de Column y Beam
    /// </summary>
    [Serializable]
    public class ConcreteSectionProps : Utility.GlobalizedObject, ICloneable
    {
        /// <summary>
        /// M�todo heredado de IClonable
        /// </summary>
        /// <returns>Regresa una copia superficial (completa en este caso) de s� mismo</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
