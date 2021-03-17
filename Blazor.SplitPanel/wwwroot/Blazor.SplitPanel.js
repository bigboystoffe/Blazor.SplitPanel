export function SetElementStyle(element, prop, value) {
    element.style[prop] = value;
    console.log(prop, value);
    console.log("style: " + element.style[prop]);

}

export function Alert(element, prop, value) {
    console.log(element, prop, value);
}
