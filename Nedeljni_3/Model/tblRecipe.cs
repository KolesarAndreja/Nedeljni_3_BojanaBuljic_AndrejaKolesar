//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_3.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRecipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRecipe()
        {
            this.tblIngredients = new HashSet<tblIngredient>();
        }
    
        public int recipeId { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int numberOfPersons { get; set; }
        public string description { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<int> authorId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblIngredient> tblIngredients { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
