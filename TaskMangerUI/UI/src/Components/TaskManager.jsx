import TaskManagerBar from "./TaskManagerBar";
import TaskManagerTitle from "./TaskManagerTitle";
import Tasks from "./Tasks";

export default function TaskManager()
{
    return( <div>
        <TaskManagerTitle/>
        <TaskManagerBar/>
        <Tasks />
         </div>
    );
}