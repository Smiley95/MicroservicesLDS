//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OptimizerV1.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public string UserID { get; set; }
        public string User_name { get; set; }
        public string User_pwd { get; set; }
        public string User_status { get; set; }
        public Nullable<int> User_isExpert { get; set; }
        public string User_Fname { get; set; }
        public string User_Lname { get; set; }
        public string User_address { get; set; }
        public string User_email { get; set; }
    }
}
