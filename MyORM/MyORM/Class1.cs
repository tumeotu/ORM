using MyORM.SQLBuilder;
using System;

namespace MyORM
{
	public class Class1
	{
        static void main(string[] args)
        {
            SinhVien sinhVien = new SinhVien();
            sinhVien.name = "sdsd";
            sinhVien.DTB = 305;
            SqlString<SinhVien> sqlString = new SqlString<SinhVien>();

            sqlString.Update(sinhVien).Where(sv => sv.name == "sff");
            Console.WriteLine(sqlString.sql);

            sqlString.save(sinhVien);
            Console.WriteLine(sqlString.sql);

            sqlString.SelectAll().Where(sv => sv.name == "ss dfd").AND(sv => sv.DTB >= 5);
            Console.WriteLine(sqlString.sql);
        }
	}
}
