using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Material
{
    /// <summary>
    /// Propiedades de dise�o de un material. Dependen del tipo de material. 
    /// Con estas propiedades se calcula si el material se rompe bajo las deformaciones calculadas.
    /// No tienen influencia sobre el an�lisis.
    /// </summary>
    [Serializable]
    public abstract class MaterialDesignProps : Utility.GlobalizedObject, ICloneable, INamed
    {
        /// <summary>
        /// Cada conjunto de propiedades de dise�o tiene un nombre, que es el mismo para 
        /// todos los materiales del mismo tipo (eg. Concreto, Acero, Aluminio).
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Este m�todo es necesario para que el Material funcione como Prototype.
        /// Hace una copia superficial (completa, en este caso) de las propiedades.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
