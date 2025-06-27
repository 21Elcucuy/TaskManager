import { useState } from "react";
import "./SignUp.css";
import { useNavigate } from "react-router-dom";
export default function SignUp() {
    const [username ,setUserName] = useState(""); 
    const [email ,setEmail] = useState("");
    const [password ,setPassword] = useState("");
    const [firstName,setFirstName] = useState("");
    const [lastName , setLastName] = useState("");
    const [loading ,setLoading] = useState(false);
    const Signup = async(username,email,password,firstName,lastName) =>
    {

            setLoading(true);
            try{
                var response = await fetch("http://localhost:5159/api/Auth/register" , { 
                    method: "POST",
                     headers:{
                     "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ UserName : username, Email : email, Password : password,FirstName : firstName, LastName :lastName })
                    
                }
                )
     
                const contentType = response.headers.get("content-type");

               if (contentType && contentType.includes("application/json")) {
               const data = await response.json();
                if(response.ok)
                {
                     const history = useHistory();
                    localStorage.setItem("token",data.token);
                    console.log("SignUp Succeful");
                    history.push("/taskmanager");
                }else(
                    alert("login failed" + data.message)

                )

               }
               else{
                  const text = await response.text();
                 console.error("Unexpected non-JSON response:", text);
               }
            }
            catch(error){
                 console.log("something went wrong" + error)
            }finally{
                setLoading(false);
            }

    }
     const handleSignUp = (e) => {
        e.preventDefault();
        Signup(username, email, password, firstName, lastName);

     };

      return (
      <div className="Signup-container ">
         <form onSubmit={handleSignUp} className="Signup-form">
          <h2>Sign in</h2>
          <input type="text" placeholder="UserName" onChange={(e) => setUserName(e.target.value)} />

          <input type="email" placeholder="Email"  onChange={(e) => setEmail(e.target.value)}/>
          
          <input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
          
          <input type="text" placeholder="First Name" onChange={(e) => setFirstName(e.target.value)} />
          
          <input type="text" placeholder="Last Name"  onChange={(e) => setLastName(e.target.value)}/>

          <button type="submit">SignUp</button>      
        </form>
    
    
    </div>
    );

}
