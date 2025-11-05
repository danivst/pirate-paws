document.addEventListener("DOMContentLoaded", function() 
{
    const email = document.getElementById("email");
    
    email.addEventListener("click", () => 
    {
        email.classList.add("clicked");

        setTimeout(() => 
        {
            email.classList.remove("clicked");
        }, 150); // delay
    });
});