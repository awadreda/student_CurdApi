






let DisableDIv = document.getElementById("disabledContent");



let listStudent = document.getElementById("listStudent");

let btnShowAll = document.getElementById("ShowAllStudents");

let btnPassedStudent = document.getElementById("PassedStudents");

async function ShowStudentList() {
  try {
    let response = await fetch("http://localhost:5218/api/Students/All");

    // console.log(response);
    let result = await response.json();

    fillTable(result);
    AddEverntsToICons();
  } catch (error) {
    console.log(`${error.message}`);
  }
}

function fillTable(list) {
  let tbody = document.getElementById("tbodyStudents");

  tbody.innerHTML = "";

  list.forEach((Student) => {
    tbody.innerHTML += `
       
       <tr class="hover:bg-slate-300 duration-300">
        <td class="px-4 py-2 border-b text-xl text-gray-700">${Student.id}</td>
        <td class="px-4 py-2 border-b text-xl text-gray-700">${Student.name}</td>
        <td class="px-4 py-2 border-b text-xl text-gray-700">${Student.age}</td>
        <td class="px-4 py-2 border-b text-xl text-gray-700">${Student.grade}</td>
        <td class="px-4 py-2 border-b flex justify-around "><i id="EditStudentIcon${Student.id}" data-studentid="${Student.id}" class="Edit-icons ri-edit-box-line font-bold text-2xl cursor-pointer hover:text-blue-800 duration-300 text-blue-600"></i> <i id="RemoveStudentIcon${Student.id}" data-studentid="${Student.id}" class="Remove-icons ri-delete-bin-line font-bold text-2xl cursor-pointer hover:text-red-800 duration-300 text-red-600"></i></td>

        </tr>

       `;
      
      
      });
       
      
      
      listStudent.appendChild(tbody);
      
    }




    function AddEverntsToICons()
    {

      
      let EditICons=  document.querySelectorAll(`.Edit-icons`)
      
      // console.log(EditICons);
      EditICons.forEach(icon => {

          icon.addEventListener("click", function () {
            
          
            console.log(this.dataset.studentid);
            CreateEditCard(this.dataset.studentid);
          });
          
        })
        
        
        
        document.querySelectorAll(`.Remove-icons`).forEach((icon) => {
          icon.addEventListener("click", function () {
            
         
            console.log(this.dataset.studentid);
            CreateRemoveCard(this.dataset.studentid);
          });
        });

        
      }
        
        
        
        


async function ShowPassedStudent() {
  try {
    let response = await fetch(
      "http://localhost:5218/api/Students/Passed"
    );

    // console.log(response);
    let result = await response.json();

    fillTable(result);
  } catch (error) {
    console.log(`${error.message}`);
  }
}

window.onload = function () {
  ShowStudentList();
};

btnShowAll.addEventListener("click", function () {
  ShowStudentList();
});

btnPassedStudent.addEventListener("click", function () {
  ShowPassedStudent();
});







 async function ShowAvargeGrade()
 {
  
  try
  {
      let response = await fetch("http://localhost:5218/api/Students/Avarge");
     
      let result = await response.json();

      console.log("averge")
       console.log(result);

       document.getElementById("AvargaeValue").innerHTML=`: ${result}`;

  }
  catch(error)
  {
    console.log("lol")
    console.log(error.message);
  }


 }


 ShowAvargeGrade();








 async function FindStudentByID()
 {

  let inputID= document.getElementById("StudentIDValue");
 
     console.log(inputID);

  if(inputID.value != "")
  {
 
    let response = await fetch(`http://localhost:5218/api/Students/${inputID.value}`);

    console.log(response);
    if(response.ok===true)
   {

     
     let result = await response.json();
     console.log(result);
     
     
     FillstudentCard(result);
     
    }

    document.getElementById("studentCard").innerHTML=`
    ${response.statusText}
    `;


  }



 }



 function FillstudentCard(Student)
 {
    let card = document.getElementById("studentCard");

    card.innerHTML = `
    
     <span id="idCard" class="bg-blue-600 text-white right-1.5 top-1.5  px-1 rounded-full absolute">${Student.id}</span>

        <div id="NameCard" class="text-xl mb-3">${Student.name}</div>

        <div id="age" class=" flex justify-between">
          <span >
            Age :
          </span>

          <span id="ageCard">${Student.age}</span>
        </div>
        
        
        <div id="grade" class=" flex justify-between">
          <span >
            Grade :
          </span>

          <span id="gradeCard">${Student.grade}</span>
        </div>
    
    `;

    

 }

 let btnFindID = document.getElementById("btnFindID");

 btnFindID.addEventListener("click",function() {


  FindStudentByID();

 })

 


  




function CreateRemoveCard(StudentID)
{

  
  let removeCard =document.createElement("div");
  
  removeCard.id = `deletDev`;
  
  removeCard.className = `absolute  z-10  bg-white  rounded-md border border-blue-200 text-center   mx-auto `;
  
  removeCard.style.cssText = `
  
  width: 500px; height: 150px; top: 250px; left:50%; transform: translate(-50%);
  
  `;
  
  
  
  removeCard.innerHTML = `

  
  <h2 class="text-3xl font-bold my-8 ">Remove Student With ID 2</h2>
  
        <div id="ConfrmDeletbtns" class="flex justify-around w-3/4 mx-auto">
        
        <button id="RemoveBtnCard${StudentID}" data-StudentID="${StudentID}" class="bg-red-600 text-white font-bold px-2 py-1 hover:bg-red-700 duration-300 rounded-md">Remove</button>
        <button id="CancelRemoveCard"  class="bg-green-600 text-white font-bold px-2 py-1 hover:bg-green-700 duration-300 rounded-md">Cancel</button>
        </div>
        
        `;
 
        DisableDIv.classList.toggle("hidden");
        document.body.append(removeCard);
        
  let RemoveBtn = document.getElementById(`RemoveBtnCard${StudentID}`);

  RemoveBtn.addEventListener("click",function(){

     DeleteStudentMethod(this.dataset.studentid);

     DisableDIv.classList.toggle("hidden");

     this.parentElement.parentElement.remove();
 
    
    })


  let CancelBtnRemoveCard = document.getElementById("CancelRemoveCard");

  

  CancelBtnRemoveCard.addEventListener("click",function(){

     DisableDIv.classList.toggle("hidden");

    this.parentElement.parentElement.remove();


  })


  




}




async function DeleteStudentMethod(StudentID)
{
  console.log(`Delete ${StudentID}`);

  try
  {

    
    let response = await fetch(`http://localhost:5218/api/Students/${StudentID}`,
      {
        
        method: "DELETE",
        
        
      }
      
      
    );
   
    if(response.ok)
      {
    
    try
    {
      let result;
          
          
            result =  await response.json();
            
            console.log(response);
            console.log(result);
            
            
          }
          catch(error)
          {
            
            result= null;
          }
        
        }
      }
      catch(error)
      {
        console.error("Error Ocaurd", error.message);

      }



      ShowStudentList();

  }







  //Addnew student

  let AddNEwBtn = document.getElementById("addNewStudent");


  AddNEwBtn.addEventListener("click",function(){


    console.log("Addnew");

    AddNewCard();


  })



  function AddNewCard()
  {

    DisableDIv.classList.toggle("hidden");


    let AddNewDiv=document.createElement("div");

    AddNewDiv.id = "AddNewDiv";

    AddNewDiv.className =
      "flex flex-col items-center absolute  z-10  bg-white  rounded-md border border-blue-200 text-center   mx-auto ";

      AddNewDiv.style.cssText = `
      
      width: 450px;  top: 250px; left:50%; transform: translate(-50%);
      
      `;


      AddNewDiv.innerHTML = `
      
      

          <h2 class="font-bold text-2xl m-3">Add New Student</h2>

          <div id="inputs">

            <div id="nameInputDiv" class="m-2 w-fit p-2">
              <label for="NameInput">Name :</label>
              <input id="NameInput" type="text" placeholder="Your Name" class="bg-slate-200 rounded-md p-1 pl-2">
            </div>
            
            
            <div id="AgeInputDiv" class="m-2 w-fit p-2">
              <label for="AgeInput" class="mr-3">Age :</label>
              <input id="AgeInput" type="number" min="0" placeholder="Your Age" class="bg-slate-200 rounded-md p-1 pl-2">
            </div>
            
            
            <div id="GradeInputDiv" class="m-2 w-fit p-2">
              <label for="GradeInput">Grade :</label>
            <input id="GradeInput" min="0" type="number" placeholder="Your Grade" class="bg-slate-200 rounded-md p-1 pl-2">
          </div>
          
        </div>

        <div id="btns" class="m-4 w-3/4 flex  justify-around">
          <button id="AddNewStudentBtn" class="  bg-blue-600 text-white font-bold px-2 py-1 hover:bg-blue-700 duration-300 rounded-md">Add New</button>
          <button id="CloseAddnew" class="bg-red-600 text-white font-bold px-2 py-1 hover:bg-red-700 duration-300 rounded-md"> Close </button>
        </div>

      
      
      `;


      document.body.append(AddNewDiv);


      document.getElementById("AddNewStudentBtn").addEventListener("click",function(){


        console.log("Added");
        AddnewStudent();

        this.parentElement.parentElement.remove();

      });

      

      
      document.getElementById("AddNewStudentBtn").addEventListener("click",function(){


        console.log("Added");
        AddnewStudent();

        this.parentElement.parentElement.remove();

      });

      



  }





//  deletel endpoint


// curl -X 'DELETE' \
  // 'http://localhost:5218/api/Students/23' \
  // -H 'accept: */*'

  // http://localhost:5218/api/Students/23




//   AddNew Student 

//   curl -X 'POST' \
//   'http://localhost:5218/api/Students' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -d '{
//   "id": 66,
//   "name": "wolf",
//   "age": 55,
//   "grade": 44
// }'









// EditUser
// curl -X 'PUT' \
//   'http://localhost:5218/api/Students/1021' \
//   -H 'accept: text/plain' \
//   -H 'Content-Type: application/json' \
//   -d '{
//   "id": 0,
//   "name": "Ahmed",
//   "age": 33,
//   "grade": 55
// }'

