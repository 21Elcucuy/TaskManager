import React, { useState } from "react";
import "./TaskItem.css";
import { useNavigate } from "react-router-dom";

const statusMap = {
  0: "InProgress",
  1: "Completed",
};

const Task = ({ task }) => {
  const { id, name, description, created, status } = task;
  const statusText = statusMap[status] || "Unknown";
  const date = new Date(created);
  const navigate = useNavigate();
  const [isUpdating, setIsUpdating] = useState(false);
  const [isDelete, setIsDeleting] = useState(false);
  const handleProgressButton = async (e) => {
    e.preventDefault();
    setIsUpdating(true);
    const newStatus = status === 0 ? 1 : 0; // Correct status toggle

    try {
      const response = await fetch(`http://localhost:5159/api/TaskItem`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify({ 
          id: id,  
          status: newStatus,
          name: name,
          description: description 
        }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Failed to update status");
      }

      // Refresh the page after successful update

        window.location.reload(); 
      
    } catch (error) {
      console.error("Status update failed:", error);
      alert(error.message || "Failed to update task status");
    } finally {
      setIsUpdating(false);
    }
  };







   const handleDeleteButton = async () => {
      setIsDeleting(true);
      try{
         console.log(id);
             const response = await fetch(`http://localhost:5159/api/TaskItem/${id}` , {
           method: "DELETE",
            headers: {
         "Authorization": `Bearer ${localStorage.getItem("token")}`,
          "Content-Type": "application/json"  
        },
                 
             });
             
       if (!response) {
      throw new Error("No response received from server");
    }      
 
    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Failed to delete task");
     
      
      }
    
      
          window.location.reload();       
      }
      catch(error){
           console.log(`somthing went wrong: ${error}`);
           
      }
      finally{
        setIsDeleting(false);
      }
   }
  return (
    <div className={`task-card ${statusText}`}>
     <div className="display-delete">
      <h3 className="task-Name">{name}</h3>
          <button className="delete-button" onClick={handleDeleteButton} disabled={isDelete}>{isDelete? "Deleting.." :"Delete"}</button>
      </div> 
      <p className="task-desc">{description}</p>
      <div className="task-footer">
        <span className="task-date">
          📅 {date.toLocaleDateString("en-US", { 
            year: "numeric", 
            month: "long", 
            day: "numeric" 
          })}
        </span>
        <button 
          className={`task-status ${statusText}`}
          onClick={handleProgressButton}
          disabled={isUpdating}
          >
          {isUpdating ? "Updating..." : statusText}
        </button>
      </div>
    </div>
  );
};

export default Task;