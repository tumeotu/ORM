using MyORM.Mapper.MapperAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM
{
    [TableAttribute("sinhvien")]
    class SinhVien
    {
        [PrimaryKeyColumn("id")]
        public int ID { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("gpa")]
        public double DTB { get; set; }
        public SinhVien(string name, int ID, double dtb)
        {
            this.ID = ID;
            this.name = name;
            this.DTB = dtb;
        }
        public SinhVien()
        {
        }
    }
}
