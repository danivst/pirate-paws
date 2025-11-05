function openPopup() 
{
    document.documentElement.style.overflow = "hidden";
    document.body.style.overflow = "hidden";

    document.getElementById("checkbox").checked = false;

    document.getElementById("popup").style.display = "block"; 
    document.getElementById("overlay").style.display = "block";
}

function closePopup()
{
    document.documentElement.style.overflow = "auto";
    document.body.style.overflow = "auto";

    document.getElementById("popup").style.display = "none";
    document.getElementById("overlay").style.display = "none";

    document.getElementById("checkbox").checked = false;

    document.getElementById("game-download").classList.add("disabled");
    document.getElementById("game-download").disabled = true;
}

function toggleDownloadButton() 
{
    const checkbox = document.getElementById("checkbox");
    const button = document.getElementById("game-download");

    if (checkbox.checked) 
    {
        button.classList.remove("disabled");
        button.disabled = false;
    } 
    else 
    {
        button.classList.add("disabled");
        button.disabled = true;
    }
}

function checkAgreement() 
{
    const checkbox = document.getElementById("checkbox");
    if (checkbox.checked) 
    {
        const downloadLink = document.createElement("a");
        downloadLink.href = "../resources/game/Pirate Paws.apk"; // path
        downloadLink.download = "Pirate Paws.apk"; // file
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);
        
        closePopup();
    }
}