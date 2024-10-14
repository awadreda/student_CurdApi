namespace testDataBase;

using System.Data.Common;
using studentDataAccessLayer;

class Program
{
    static void Main(string[] args)
    {
    
    
          if (studentDataAccessLayer.StudentData.IsFound(99))
           {

            Console.WriteLine("Yes"); 
           }else
           {

            Console.WriteLine("NO");
           }
    
        //         StudentDTO STDO=new StudentDTO(25,"walid",55,55);

             
        // // int ID = studentDataAccessLayer.StudentData.AddNewStudent(STDO);
             
             
        //      if(studentDataAccessLayer.StudentData.UpdateStudent(STDO))
        //      {
                







        // var st = studentDataAccessLayer.StudentData.GetStudentByID(STDO.ID);



        // Console.WriteLine($" {st.ID}, {st.name},  {st.Age}, {st.Grade}");

        //      }
        //      else
        //      {


           
           
        //        Console.WriteLine("Faild ya malom");
         
        //      }


       
        // if(studentDataAccessLayer.StudentData.DeleteStudent(25))
        // {

        //     Console.WriteLine("Done it's what it is ");
        // }
        // else
        // {

        //     Console.WriteLine("Falid ya molem");
        // }




        // List<StudentDTO>  Students = studentDataAccessLayer.StudentData.GetAllStudents();


        //     Students.ForEach( st =>
        //         Console.WriteLine($" {st.ID}, {st.name},  {st.Age}, {st.Grade}")
        //     );
    

    

    
    
    
    
    
    
    
    
    }
}
