
using System.Data.SqlClient;

using System.Linq;
using System.Collections.Generic;




namespace studentDataAccessLayer;
   public class StudentDTO 
   {
     
        public StudentDTO(int ID  ,string Name , int Age ,int Grade)
        {

         this.ID = ID;
         this.name = Name;
         this.Age = Age;
         this.Grade = Grade;
        }


      public int ID  { get; set; }

      public  string  name {get; set;}     
      public   int Age { get; set; }

    public int Grade { get; set; }


   }

public class StudentData

{

    static string connectionString = "Server=.;Database= MyBankDB ;User Id=sa;Password=sa123456;";



  public static List<StudentDTO> GetAllStudents()
  {
        List<StudentDTO> students = new List<StudentDTO>();


   SqlConnection connection = new SqlConnection(connectionString);

      string query = $"  SELECT  *  FROM [StudentAPI].[dbo].[Students]";


      SqlCommand command = new SqlCommand(query, connection);


      try
      {
         connection.Open();

         SqlDataReader reader = command.ExecuteReader();

         while(reader.Read())
         {
                students.Add(new StudentDTO
            (

               reader.GetInt32(reader.GetOrdinal("studentID")),
                  reader.GetString(reader.GetOrdinal("Name")) ,
                reader.GetInt32(reader.GetOrdinal("Age")),
                reader.GetInt32(reader.GetOrdinal("Grade"))

            ));

            }

            reader.Close();

      }
    
      finally
      {

         connection.Close();

      }


      return students;



  }
     


  public static List<StudentDTO> GetPassedStudents()
  {
        List<StudentDTO> students = new List<StudentDTO>();


   SqlConnection connection = new SqlConnection(connectionString);

      string query = $"  SELECT * FROM [StudentAPI].[dbo].[Students] where Students.Grade < 85;";


      SqlCommand command = new SqlCommand(query, connection);


      try
      {
         connection.Open();

         SqlDataReader reader = command.ExecuteReader();

         while(reader.Read())
         {
                students.Add(new StudentDTO
            (

               reader.GetInt32(reader.GetOrdinal("studentID")),
                  reader.GetString(reader.GetOrdinal("Name")) ,
                reader.GetInt32(reader.GetOrdinal("Age")),
                reader.GetInt32(reader.GetOrdinal("Grade"))

            ));

            }

         reader.Close();

      }
    
      finally
      {
      

         connection.Close();

      }


      return students;



  }
     






  public static double GetAvargeStudents()
  {

      double Averge = -1;

   SqlConnection connection = new SqlConnection(connectionString);

      string query = $"SELECT AVG(Students.Grade) as Averge_Valeu FROM [StudentAPI].[dbo].[Students] ";


      SqlCommand command = new SqlCommand(query, connection);


      try
      {
         connection.Open();

         // SqlDataReader reader = command.ExecuteReader();

         object result =  command.ExecuteScalar();

       if(result != DBNull.Value)
       {

         Averge =Convert.ToDouble(result);
       }


      }
    
      finally
      {

         connection.Close();

      }


      return Averge ;



  }






  public static StudentDTO GetStudentByID(int ID)
  {

      StudentDTO? student = null;

   SqlConnection connection = new SqlConnection(connectionString);

      string query = "SELECT * FROM [StudentAPI].[dbo].[Students] where studentID =@ID; ";


      SqlCommand command = new SqlCommand(query, connection);
      
      command.Parameters.AddWithValue("@ID", ID);

      try
      {
         connection.Open();

         SqlDataReader reader = command.ExecuteReader();

        if(reader.HasRows)
        {
            reader.Read();

           student = new StudentDTO(
            reader.GetInt32(reader.GetOrdinal("studentID")),
             reader.GetString(reader.GetOrdinal("Name")),
           reader.GetInt32(reader.GetOrdinal("Age")),
            reader.GetInt32(reader.GetOrdinal("Grade"))
            );


        }


         reader.Close();
      }
   
    
      finally
      {

         connection.Close();

      }


#pragma warning disable CS8603 // Possible null reference return.
        return student ;
#pragma warning restore CS8603 // Possible null reference return.



    }


    

    public static int  AddNewStudent(StudentDTO student)
    {

      int StudentID = 0;
     
     SqlConnection connection = new SqlConnection(connectionString);

      string query = @"

USE [StudentAPI]


INSERT INTO [dbo].[Students]
           ([Name]
           ,[Age]
           ,[Grade])
     VALUES
           (@Name
           ,@Age
           ,@Grade);
            SELECT SCOPE_IDENTITY();

  
";

    SqlCommand command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@Name", student.name);
    command.Parameters.AddWithValue("@Age", student.Age);
    command.Parameters.AddWithValue("@Grade", student.Grade);



       try
       {

         connection.Open();

         object result = command.ExecuteScalar(); 

         if(result != null)
         {

            StudentID=Convert.ToInt32(result);
         }



       }
       catch(Exception ex)
       {

         Console.WriteLine(ex.Message);
       }
       finally
       {

         connection.Close();
       }


       return StudentID;



    }






               public static bool UpdateStudent(StudentDTO student)
               {

      int rowsAffect = 0;



      SqlConnection connection = new SqlConnection(connectionString);


      string query = @"
      
      USE [StudentAPI]
  UPDATE [dbo].[Students]
   SET [Name] = @Name
      ,[Age] = @Age
      ,[Grade] = @Grade
    WHERE studentID = @ID
    
        
      ";

      SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Name", student.name);
      command.Parameters.AddWithValue("@Age", student.Age);
      command.Parameters.AddWithValue("@Grade", student.Grade);
      command.Parameters.AddWithValue("@ID", student.ID);


         try
         {

         connection.Open();

           rowsAffect = command.ExecuteNonQuery();



         }
         catch(Exception ex)
         {
            Console.WriteLine(ex.Message);

            
         }
         finally{

         connection.Close();
         }

         return (rowsAffect >0);






               }





      public  static bool DeleteStudent(int ID)
      {


         SqlConnection connection = new SqlConnection(connectionString);

      string query = @"
      USE [StudentAPI]
       DELETE FROM [dbo].[Students]
       WHERE Students.studentID =@ID; 
      
      ";


   SqlCommand command = new SqlCommand(query, connection);

         int rowsAffect = 0;
      command.Parameters.AddWithValue("@ID", ID);


      try
      {
            connection.Open();

            rowsAffect=command.ExecuteNonQuery();


      }
      catch(Exception ex)
         {
            Console.WriteLine(ex.Message);

            
         }
      
      
      finally
      {
         connection.Close();


      }

      return (rowsAffect >0);


      
      
      
      }




         public static bool IsFound(int ID)
         {

            bool IsFound = false;



   SqlConnection connection = new SqlConnection(connectionString);

      string query = "SELECT A=10 FROM [StudentAPI].[dbo].[Students] where studentID =@ID; ";


      SqlCommand command = new SqlCommand(query, connection);
      
      command.Parameters.AddWithValue("@ID", ID);

      try
      {
         connection.Open();

         SqlDataReader reader = command.ExecuteReader();

        if(reader.HasRows)
        {

            IsFound = true;
            

        }


      }
   
    
      finally
      {

         connection.Close();

      }


      return IsFound;


            

         }


     
   // USE [StudentAPI]
// GO

// DELETE FROM [dbo].[Students]
//       WHERE Students.studentID =26; 
// GO




// select * from Students;

// USE [StudentAPI]
// GO



// UPDATE [dbo].[Students]
//    SET [Name] = 'lolo'
//       ,[Age] = 14
//       ,[Grade] = 65
//  WHERE studentID = 3
// GO










        




}
