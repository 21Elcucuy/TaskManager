import { useState, useEffect } from "react";
import TaskItem from "./TaskItem"; // Ensure this import exists
import "./Tasks.css"
import { useNavigate } from "react-router-dom";
function TasksContainer() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [name , setName] = useState("");
  const [description ,setDesciption] = useState("");

 const navigate = useNavigate();

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        const response = await fetch("http://localhost:5159/api/TaskItem", {
  headers: {
    "Authorization": `Bearer ${localStorage.getItem("token")}`,
  },
});
        
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json(); // <-- Add await
        console.log(data);
        setTasks(data);
      } catch (err) {
        console.error("Failed to fetch tasks:", err);
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchTasks();
  }, []); 

  function handleAddButton()
  {
        navigate("/additem");        
  }
  return (
    <div className="Tasks-Container">
      <h3>Tasks</h3>
      <button className="add-button" onClick={handleAddButton}>Add Task</button>
      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : tasks.length === 0 ? (
        <p>No tasks found.</p>
      ) : (
        tasks.map((task) => <TaskItem key={task.id} task={task}  />)
      )}
    </div>
  );
}

export default TasksContainer;