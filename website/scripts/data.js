const tocButton = document.getElementById("toc-button");
const topButton = document.getElementById("top-button");
const bottomBar = document.getElementById("bottom-bar");

let cat = null;
export function setCat(value)
{
  cat = value;
}
export function getCat() 
{
  return cat;
}

let targetRotationY = -Math.PI / 2;
export function setTargetRotationY(value) 
{
    targetRotationY = value;
}
export function getTargetRotationY() 
{
    return targetRotationY;
}

export { tocButton, topButton, bottomBar };