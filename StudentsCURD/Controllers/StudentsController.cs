using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentsCURD.Models;
// using StudentsCURD.MyData;
using StudentBuisnessLayer;
using studentDataAccessLayer;




namespace StudentsCURD
{
    [ApiController]
    [Route("api/Students")]
    public class StudentsController : ControllerBase
    {

       [HttpGet("All",Name="GetAllStudents")]
       
       [ProducesResponseType(StatusCodes.Status200OK)]

       public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
       {

            var Students = StudentBuisnessLayer.Student.GetAllStudents();
 
               if(Students.Count ==0)
               {
                return NotFound("Not Found Students");
               }

            return Ok(Students);
       }
        

        [HttpGet("Passed",Name="GetPassedStudents")]
         [ProducesResponseType(StatusCodes.Status200OK)]


        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {

       
            var Students = StudentBuisnessLayer.Student.GetAllStudents();
 
               if(Students.Count ==0)
               {
                return NotFound("Not Found Students");
               }

            return Ok(Students);
       

        }

        [HttpGet("Avarge", Name="GetAvarge")]

          [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public ActionResult<double> GetAvargeGradeSutdents()
         {


            var AvargeGrade = studentDataAccessLayer.StudentData.GetAvargeStudents();
          
            return Ok (Math.Round(AvargeGrade, 2));

         }



        [HttpGet("{id}",Name = "GetStudentByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public ActionResult<StudentDTO> GetStudentByID(int id)
        {
            if (id < 1)
               {
                return BadRequest($"Not Vaild ID {id}");
               }


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            StudentBuisnessLayer.Student Student = StudentBuisnessLayer.Student.Find(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (Student == null)
           {

                return NotFound($"Student with ID {id} Not Found");
           }

            StudentDTO SDTO = Student.SDTO;

            return Ok(SDTO);


         }






         [HttpPost(Name="AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudent)
        {
            if (newStudent == null || string.IsNullOrEmpty(newStudent.name) || newStudent.Age < 0|| newStudent.Grade <0)
            {

                return BadRequest("Invalid student Data. ");
            }


            StudentBuisnessLayer.Student student = new StudentBuisnessLayer.Student(new StudentDTO(newStudent.ID,newStudent.name,newStudent.Age,newStudent.Grade));

            student.Save();

            newStudent.ID = student.ID;

            return CreatedAtRoute("GetStudentByID", new { id = newStudent.ID }, newStudent);

            

            


        }




        [HttpDelete("{id}", Name = "DeletStudent")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public ActionResult DeleteStudent(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accpted ID {id}");

            }


                     if(StudentBuisnessLayer.Student.IsFound(id))
                     {

                 StudentBuisnessLayer.Student.DeleteStudent(id);
                return Ok($"Student With ID {id} has  been Deleted");

                     }



            return NotFound($"The Student with id  {id} not Found");

       }            

    
     [HttpPut("{id}",Name ="UpdateStudent")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


    public ActionResult<StudentDTO> UpdateStudent(int id ,StudentDTO UpdatedStudent)
    {
        if(id <1 || UpdatedStudent == null || string.IsNullOrEmpty(UpdatedStudent.name) || UpdatedStudent.Age <0 || UpdatedStudent.Grade <0)
        {

                return BadRequest("Invalid Student data");

        }

            var student = StudentBuisnessLayer.Student.Find(id);

            if(student == null)
            {
                return NotFound($"Student With ID {id} Not Found");



            }

            student.name = UpdatedStudent.name;
            student.Age = UpdatedStudent.Age;
            student.Grade = UpdatedStudent.Grade;

            if(student.Save())
            {

            return Ok(student.SDTO);

            }

            return StatusCode(500, new { message = "Error updating Student" });
    }





          

    }
}