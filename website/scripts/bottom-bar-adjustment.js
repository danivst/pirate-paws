window.addEventListener("resize", adjust);
window.addEventListener("load", adjust);

function adjust() 
{
    const line = document.getElementById("line");

    if (window.innerWidth <= 420) 
    {
        line.innerHTML = "| <br>";
    } 
}