using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Load
{
    /// <summary>
    /// Clase que representa un caso de an�lisis.
    /// El tipo de caso est� determinado por las propiedades que se le asignen.
    /// Algunos tipos son: est�tico, modal y espectro de respuesta.
    /// </summary>
    [Serializable]
    public class AnalysisCase : AbstractCase
    {
        private AnalysisCaseProps properties;

        /// <summary>
        /// Constructora que da valores iniciales para el nombre y asigna propiedades default (StaticCaseProps).
        /// </summary>
        /// <param name="name"></param>
        public AnalysisCase(string name)
            : this(name, new StaticCaseProps())
        {
        }

        /// <summary>
        /// Constructora que da valores iniciales para el nombre y las propiedades.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="props"></param>
        public AnalysisCase(string name, AnalysisCaseProps props)
            : base(name)
        {
            properties = props;
        }

        /// <summary>
        /// Propiedades del AnalysisCase. Definen el tipo de caso de an�lisis
        /// </summary>
        public AnalysisCaseProps Properties
        {
            get
            {
                return properties;
            }
            set
            {
                if (value != null && value != properties)
                {
                    Model.Instance.Undo.Change(this, properties, GetType().GetProperty("Properties"));
                    properties = value;
                }
            }
        }
    }
}
