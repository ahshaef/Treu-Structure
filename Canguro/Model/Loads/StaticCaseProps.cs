using System;
using System.Collections.Generic;
using System.Text;

namespace Canguro.Model.Load
{
    /// <summary>
    /// Clase que representa las propiedades de un caso lineal est�tico.
    /// No tiene propiedades espec�ficas, por lo que s�lo implementa la propiedad Name.
    /// </summary>
    [Serializable]
    public class StaticCaseProps : AnalysisCaseProps
    {
        protected List<StaticCaseFactor> loads = new List<StaticCaseFactor>();

        /// <summary>
        /// Propiedad que regresa el nombre del caso de an�lisis est�tico, en el idioma correcto.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return Culture.Get("StaticCaseName");
            }
        }

        public AnalysisCase DependsOn
        {
            get { return null; }
        }

        /// <summary>
        /// LoadCase and factors list. It's copied when it's read or set.
        /// </summary>
        public List<StaticCaseFactor> Loads
        {
            get
            {
                return new List<StaticCaseFactor>(loads);
            }
            set
            {
                Model.Instance.Undo.Change(this, loads, this.GetType().GetProperty("Loads"));
                loads = new List<StaticCaseFactor>();
                foreach (StaticCaseFactor f in value)
                {
                    if (f.AppliedLoad is LoadCase || f.AppliedLoad is AccelLoad)
                        loads.Add(f);
                }
            }
        }
    }
}
