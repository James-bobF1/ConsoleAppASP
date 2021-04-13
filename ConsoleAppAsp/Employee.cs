using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppAsp
{
    class Employee
    {
        //ФИО, должность, email, телефон, зарплата, возраст
        string Fio;
        string Post;
        string Email;
        string Phone;
        float Salary;
        decimal age;
        public decimal Age 
        { 
            set
            {
                age = (value > 0 && value < 200) ? value : 0;
            }
            get 
            {
                return age;
            } 
        }
        public Employee(string fio, string post,string email, string phone, float salary, decimal age)
        {
            Fio = fio;
            Post = post;
            Email = email;
            Phone = phone;
            Salary = salary;
            Age = age;
        }
        public override string ToString()
        {
            return $"{Fio} {Post} {Email} {Phone} Salary {Salary} Age {Age}";
        }
    }
}
