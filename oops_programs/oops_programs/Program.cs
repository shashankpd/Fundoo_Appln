/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

//11.Design an object-oriented system for a virtual classroom. Consider classes like Student, Teacher, and Course.
//Implement features such as enrollment, grading, and communication between students and teachers.
//Utilize concepts like inheritance, encapsulation, and polymorphism to model the relationships and behaviors in this system.

/*namespace oops_programs
{
    public class classroom
    {

        public int id { get; set; }
        public char grade { get; set; }
        public string name { get; set; }

        public  classroom( int id,char grade,string name)
        {
           this.id = id;
                
           this.grade=grade;

           this.name = name;
        
        }

        
        

      public class student
        {
            public void stdtls()
            {


                Console.WriteLine("i am student class");
            }
        
        }

        public class teacher : student
        {
            public void tchdtls(string name, int id , char grade)
            { 

          
            Console.WriteLine("my name is "+name+"id: " +id+"grade is: "+grade);
            }
        
        }

        public class course : teacher
        {
            public void crsdtls()
            { 
            Console.WriteLine("i am course class");
            }
        }

        static void Main(string[] args)
        {

            student obj = new student();

            course obj2 = new course();

            obj.stdtls();
            obj2.crsdtls();

            

        }
    }
}
*/

/*using System;

public abstract class Abst
{
    
    public abstract void Sup();
}

public class Sub : Abst
{
    
    public override void Sup()
    {
        Console.WriteLine("I am abstraction");
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        Sub obj = new Sub();
        
        obj.Sup();
    }
}*/

/*abstract class Ademo
{
    public abstract void demo();
    public void logic()
    {
        Console.WriteLine("logic method");
    }
}
class Child : Ademo
{
    public override void demo()
    {
        Console.WriteLine("demo method");
    }
}*/

/*using System;


abstract class Ademo
{
    public abstract void demo(); 

    public void logic()
    {
        Console.WriteLine("logic method"); 
    }
}


class Child : Ademo
{
    public override void demo()
    {
        Console.WriteLine("demo method"); 
    }
}

class Program
{
    static void Main(string[] args)
    {
        Child obj = new Child(); 
        obj.demo(); 
        obj.logic(); 
    }
}*/
/*
using System;

// First interface
public interface A
{
    void Meth1();
    void Meth2();
}

// Second interface
interface B
{
    void Meth3();
    void Meth4();
}

// Class C implementing both interfaces
public class C : A, B
{
    public void Meth1()
    {
        Console.WriteLine("Meth1 implementation");
    }

    public void Meth2()
    {
        Console.WriteLine("Meth2 implementation");
    }

    public void Meth3()
    {
        Console.WriteLine("Meth3 implementation");
    }

    public void Meth4()
    {
        Console.WriteLine("Meth4 implementation");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating an object of class C
        C obj = new C();

        // Invoking methods of class C
        obj.Meth1();
        obj.Meth2();
        obj.Meth3();
        obj.Meth4();
    }
}*/



//11.Design an object-oriented system for a virtual classroom. Consider classes like Student, Teacher, and Course.
//Implement features such as enrollment, grading, and communication between students and teachers.
//Utilize concepts like inheritance, encapsulation, and polymorphism to model the relationships and behaviors in this system.

/*using System;
using System.Xml.Linq;

public class classroom
{ 
    int id;
    string name;

    public classroom(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

public class student
{
    public void Enrollment(int id, string name)
    {
  
    Console.WriteLine($"Enrollment details: id= {id}, name = {name}");

    Console.WriteLine(name +" hii");


    }
}

public class teacher : student
{
    public void grading(int id, string name)
    {

     Console.WriteLine($"grading of student is: name={name},and id is: id={id}");

        Console.WriteLine("hello "+name);
    }
}

public class course 
{
    public void takcourse(string name, string crs)
    {
        Console.WriteLine($"{name}  chose {crs}");
    
    }


}

class program
{
    public static void Main(string[] args)
    { 
    classroom cls = new classroom(10,"shashank");

    student studt = new student();
        studt.Enrollment(1, "shashank");

        teacher tchr=new teacher();
        tchr.grading(1,"shashank");
        tchr.Enrollment(2, "nandini");

        course course = new course();
        course.takcourse("shashank", "maths");
    
    }
}*/


//12.Create an object-oriented model for a banking system that supports various account types (e.g., savings, checking).
//Implement transactional operations like deposits, withdrawals, and transfers between accounts.
//Use inheritance to represent different account types and encapsulation to protect sensitive information.

/*using System;
using System.Runtime.Remoting.Channels;

public class SavingAcc
{
   public int accno { get; set; }
    public decimal initbalance { get; set; }  
    public string holder { get; set; }

    public  SavingAcc(int accno, int balance,string holder)
    { 
        this.accno = accno;
        this.initbalance = balance;
        this.holder = holder;
    
    }

    public void Deposit(int amount)
    {    
       
        initbalance = amount + initbalance;

        Console.WriteLine($"{accno}:Dear {holder} the total amount is {initbalance}");
    
    }

    public void Withdrawal(int amount)
    { 
     initbalance = initbalance - amount;
        Console.WriteLine($"the total amount is {initbalance}");
    }

}

public class CheckingAcc 
{

    public void Checkbalance(SavingAcc acc)
    {

        Console.WriteLine($"Your Account Balance is{acc.initbalance}");
    }
}

class program
{
    public static void Main(string[] Args)
    { 
    SavingAcc acc = new SavingAcc(123, 2000,"shashank");
        acc.Deposit(100);
        acc.Withdrawal(200);

        CheckingAcc acc2 = new CheckingAcc();

        acc2.Checkbalance(acc);

    
    }
}*/