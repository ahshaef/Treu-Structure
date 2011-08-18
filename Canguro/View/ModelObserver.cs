using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.View
{
    /// <summary>
    /// Interfaz para los objetos que se suscribir�n al evento Model.ModelChanged
    /// siguiendo el patr�n Observer.
    /// 
    /// Por convenci�n se debe modificar la suscripci�n al evento 
    /// Model.ModelChanged al cambiar el estado de Enabled
    /// </summary>
    public interface ModelObserver
    {        
        /// <summary>
        /// Propiedad de lectura escritura para cambiar el estado del observador,
        /// as� como su suscripci�n en Model.ModelChanged
        /// </summary>
        bool Enabled
        {
            get;
            set;
        }
    }
}
