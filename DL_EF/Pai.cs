//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pai
    {
        public Pai()
        {
            this.Estadoes = new HashSet<Estado>();
        }
    
        public int IdPais { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Estado> Estadoes { get; set; }
    }
}
