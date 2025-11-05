import * as THREE from "three";
import { GLTFLoader } from "three/addons/loaders/GLTFLoader.js";
import { getCat, setCat, getTargetRotationY } from "./data.js";

const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

const canvas = document.getElementById("catCanvas");
const renderer = new THREE.WebGLRenderer({ canvas, alpha: true });  // alpha - transparent background
renderer.setSize(window.innerWidth, window.innerHeight);
renderer.setPixelRatio(window.devicePixelRatio);

const light = new THREE.DirectionalLight(0xffffff, 2);
light.position.set(1, 1, 1).normalize();
scene.add(light);

const loader = new GLTFLoader();
loader.load("../resources/model/cat.glb", function (gltf) 
{
    const cat = gltf.scene;
    cat.rotation.y = getTargetRotationY(); // to look at the face initially
    scene.add(cat);
    setCat(cat);

    const box = new THREE.Box3().setFromObject(cat);
    const center = new THREE.Vector3();
    box.getCenter(center);

    const overheadLight = new THREE.DirectionalLight(0xffffff, 1);
    overheadLight.position.set(0, 5, 0);
    overheadLight.target.position.set(0, 0, 0); 
    scene.add(overheadLight);
    scene.add(overheadLight.target);

    light.castShadow = true;
    light.shadow.bias = 0.001; // reducing shadows
    light.shadow.mapSize.width = 1024;  // better shadow quality
    light.shadow.mapSize.height = 1024;

    // ambient light for overall brightness
    const ambientLight = new THREE.AmbientLight(0xffffff, 1);
    scene.add(ambientLight);
});

// render loop
function animate() 
{
    requestAnimationFrame(animate);
    const cat = getCat();
    const targetRotationY = getTargetRotationY();

    if (cat) 
    {
        const lerpSpeed = 0.1;
        const delta = targetRotationY - cat.rotation.y;

        if (Math.abs(delta) > 0.001) 
        {
            cat.rotation.y += delta * lerpSpeed;
        } 
        else 
        {
            cat.rotation.y = targetRotationY;
        }

        const box = new THREE.Box3().setFromObject(cat);
        const center = new THREE.Vector3();
        box.getCenter(center);

        const radius = 5; // fixed distance from the cat
        const angle = cat.rotation.y + Math.PI; // angle to rotate the camera

        // camera's position calculated using cat's position
        camera.position.x = center.x + radius * Math.cos(angle);
        camera.position.z = center.z + radius * Math.sin(angle);
        camera.position.y = center.y + 1; // right above the cat

        camera.lookAt(center.x, center.y + 1, center.z);
    }
    renderer.render(scene, camera);
}
animate();