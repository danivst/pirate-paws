document.addEventListener("DOMContentLoaded", function () 
{
    fetch("../resources/policies/terms-and-conditions.txt").then(response => 
        {
            if (!response.ok) 
            {
                throw new Error("Network response was not ok");
            }
            return response.text();
        }).then(data => 
        {
            document.getElementById("text-container").innerHTML = data;
        });
});