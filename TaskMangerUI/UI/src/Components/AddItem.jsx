import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./AddItem.css"
export default function AddItem()
{
    const [name , setName ] = useState("");
    const [description , setDesciption] =useState("");
    const [loading ,setLoading] =useState(false);
    const navigate = useNavigate();
    const CreateTaskItem = async(name,description) =>
     {
        setLoading(true)
         console.log("adding .."+ name ,description);
        try{
           const response = await fetch("http://localhost:5159/api/TaskItem" , {
                method:"Post",
               headers: {
        "content-type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`,
        },
        body :JSON.stringify({name,description}),
            })
             const contentType = response.headers.get("content-type");
        
      if (contentType && contentType.includes("application/json")) {
       
        var data = await response.json;
          if(response.ok)
          {
         console.log("Add Complete:", data);
           navigate("/taskmanager"); 
          }
          else{
            alert("error" + data.message);
          }
    }
        }
        catch(error)
        {
            console.log("Something went wrong : " + error);
            
        }
        finally {
            setLoading(false);
        }
        } 

    function handleAddItem(e)
    {  
        e.preventDefault();
   
       CreateTaskItem(name,description)
    }
    return (
     <div className="AddItem-container">
      <form onSubmit={handleAddItem} className="AddItem-form">
        <h2>Add Item</h2>
        <input
          type="text"
          placeholder="Task Name "
          required
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <input
          type="text"
          placeholder="description"
          required
          value={description}
          onChange={(e) => setDesciption(e.target.value)}
        />

        <button type="submit" disabled={loading}>
          {loading ? "Adding...." : "Add Item"}
        </button>
      </form>
    </div>);
}