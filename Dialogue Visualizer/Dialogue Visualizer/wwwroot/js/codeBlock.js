class CodeBlockManager {
    currentLanguage = "Json";
    supportedLanguages = ["Json", ""];

    codeText = "";
    #codeConverted = null;

    constructor() {
        
    }

    parseCode() {
        if (this.supportedLanguages.find(x => x.toUpperCase() == this.currentLanguage.toUpperCase())) {
            switch (this.currentLanguage) {
                case "Json":
                    let codeBlock = document.getElementById("code-block-json");
                    codeBlock.innerHTML = this.#highlightJson();
                    break;
                default:
            }
        }
    }

    #highlightJson(indent = 2) {
        const keywords = ['true', 'false', 'null'];
        const regex = new RegExp(`(${keywords.join('|')})`, 'g');

        const lines = this.codeText.split('\n');

        const jsonHtml = this.codeText
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(regex, '<span class="json-keyword">$1</span>')
            .replace(/(".*?"|[\d.]+|\[|\]|\{|\}|:|,)/g, match => {
                let cls = 'json-other';
                if (/^"/.test(match)) {
                    if (/:$/.test(match)) {
                        cls = 'json-key';
                        match = match.replace(/:/, '<span class="json-colon">:</span> ');
                    } else {
                        cls = 'json-string';
                        match = match.replace(/\n/g, `\n${' '.repeat((lines.findIndex(line => line.includes(match)) + 1) * indent)}`);
                    }
                } else if (/true|false|null/.test(match)) {
                    cls = 'json-bool';
                } else if (/[\[\]{}]/.test(match)) {
                    cls = 'json-punct';
                } else if (/[\d.]+/.test(match)) {
                    cls = 'json-number';
                }
                console.log(cls);
                return `<span class="${cls}">${match}</span>`;
            });
        return jsonHtml;
    }

}

