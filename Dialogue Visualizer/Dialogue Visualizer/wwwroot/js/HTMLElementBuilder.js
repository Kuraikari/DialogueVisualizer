class HTMLElementBuilder {

    id = "";
    styles = [{property: "", value: ""}];
    size = { w: 0, h: 0 };
    position = { x: 0, y: 0 };

    text = "[TEXT]";

    #element;

    constructor(id, styles, size, position) {
        this.id = id;
        this.styles = styles;
        this.size = size;
        this.size.w = size.w;
        this.size.h = size.h;

        this.position = position;
    }

    GetElement() {
        return this.#element;
    }

    CreateElement() {
        let el = document.createElement("div");
        el.id = this.id;
        el.classList.add("dialogue-block");

        this.styles.forEach(style => {
            el.style.setProperty(style.property, style.value);
        });

        el.style.width = this.size.w;
        el.style.height = this.size.h;

        el.style.top = this.position.y + "px";
        el.style.left = this.position.x + "px";

        el.setAttribute("posX", this.position.x);
        el.setAttribute("posY", this.position.y);


        let elContent = document.createElement("div");
        elContent.classList.add("dialogue-block-content");
        elContent.textContent = this.text;

        let elHeader = document.createElement("div");
        elHeader.id = this.id + "header";
        elHeader.classList.add("header");

        el.append(elHeader, elContent);

        this.#element = el;

        return this;
    }
}
