//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace huynh_tien_thang_c2109i2_26_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblExam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public Nullable<double> ExamMark { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
        public Nullable<int> StuId { get; set; }
        public Nullable<int> CouId { get; set; }
        public string Comment { get; set; }
        public Nullable<int> MarkPass { get; set; }
    
        public virtual TblCourse TblCourse { get; set; }
        public virtual TblStudent TblStudent { get; set; }
    }
}
