using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Section
{
    /// <summary>
    /// Interfaz com�n que usan todas las secciones.
    /// La primera versi�n s�lo incluye FrameSection's, pero esta interfaz ser� necesaria en versiones posteriores.
    /// </summary>
    public interface Section : INamed
    {
        /// <summary>
        /// El nombre de la secci�n. Cada secci�n debe tener un nombre �nico en el cat�logo en el que se encuentra.
        /// SectionManager debe asegurarse de esto.
        /// </summary>
        new string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Specific to each section, includes name, material and other basic properties.
        /// </summary>
        string Description
        {
            get;
        }
        /// <summary>
        /// La forma de la secci�n. Se usa para distinguir los diferentes tipos de secciones.
        /// </summary>
        string Shape
        {
            get;
        }

        /// <summary>
        /// Cada secci�n va ligada a un material (ie. Una secci�n no puede aparecer con dos materiales diferentes).
        /// </summary>
        Material.Material Material
        {
            get;
            set;
        }
    }
}
