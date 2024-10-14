using System.Data;

using studentDataAccessLayer;


namespace StudentBuisnessLayer
{

public class Student
{

 public enum enMode{AddNew =0 , Update =1};

    public enMode mode = enMode.AddNew;

    public StudentDTO SDTO  {
      get { return (new StudentDTO(this.ID, this.name, this.Age, this.Grade)); } 
    }
 
     public  int ID { get; set; }

     public  string name { get; set; }

     public int Age { get; set; }
     public int Grade { get; set; }


    public Student(StudentDTO SDTO, enMode cMode = enMode.AddNew)
    
   {

      this.ID = SDTO.ID;
      this.name = SDTO.name;
      this.Age = SDTO.Age;
      this.Grade = SDTO.Grade;

      mode = cMode;
    
   }



   public static List<StudentDTO> GetAllStudents()
   {

        return studentDataAccessLayer.StudentData.GetAllStudents();

   }



   public static List<StudentDTO> GetPassedStudents()
   {

        return studentDataAccessLayer.StudentData.GetPassedStudents();

   }



   public static double  GetAvergeStudentGrade()
   {

      return studentDataAccessLayer.StudentData.GetAvargeStudents();
   }


    
    // public static StudentDTO GetStudentByID(int ID)
    // {

    //   return studentDataAccessLayer.StudentData.GetStudentByID(ID);

    // }


    public static Student? Find(int ID)
    {
      StudentDTO SDTO = studentDataAccessLayer.StudentData.GetStudentByID(ID);

      if(SDTO != null)
      {

        return new Student(SDTO, enMode.Update);
      }
      else
      {
        return null;
      }

    }




   private   bool _AdddNewStudent()
    {

       this.ID = studentDataAccessLayer.StudentData.AddNewStudent(SDTO);

      return (this.ID != -1);
    }


   private bool _UpdataStudent()
   {
      return StudentData.UpdateStudent(SDTO);
      
   }


    public bool Save()
    {

    switch(mode)
    {
      case enMode.AddNew:
      {

      if(_AdddNewStudent())
      {
     mode = enMode.Update;
     return true;

      }
      else
      {
     return false;
      }

      }
      
      case enMode.Update:
      {
            return _UpdataStudent();
      }

           }


           return false;

    }

    

    public static bool DeleteStudent(int ID)
    {

      return studentDataAccessLayer.StudentData.DeleteStudent(ID);
    }


   public static bool IsFound(int ID)
   {

    return studentDataAccessLayer.StudentData.IsFound(ID);


   }
     

}








}