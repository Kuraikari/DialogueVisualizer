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

    CreateDivElement(id, ...cls) {
        let el = document.createElement("div");
        el.classList.add(cls);
        el.id = id;
        return el;
    }

    CreateElement() {
        let el = this.CreateDivElement(this.id, "dialogue-block");

        this.styles.forEach(style => {
            el.style.setProperty(style.property, style.value);
        });

        el.style.width = this.size.w;
        el.style.height = this.size.h;

        el.style.top = this.position.y + "px";
        el.style.left = this.position.x + "px";

        el.setAttribute("posX", this.position.x);
        el.setAttribute("posY", this.position.y);

        let elContent = this.CreateDivElement(this.id + "-content", "dialogue-block-content");
        elContent.textContent = this.text;

        let elHeader = this.CreateDivElement(this.id + "-header", "dialogue-block-header");

        el.append(elHeader, elContent);

        this.#element = el;

        return this;
    }

    CreateContainer(name) {
        let el = document.createElement("div");
        el.id = name + '-scene';
        el.classList.add("dialogue-scene");

        el.append(this.#element);

        this.#element = el;
        return this;
    }
}
