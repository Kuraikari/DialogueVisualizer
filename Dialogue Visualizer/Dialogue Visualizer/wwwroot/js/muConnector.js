class muConnector {
    constructor(params) {
        if (typeof (params) == "undefined") { return false }; // If no params then abandon.
        // Process input params.
        var ele1 = params.ele1 || '';   // First element to link
        var ele2 = params.ele2 || '';   // Second element to link
        if (ele1.length === 0 || ele2.length === 0) { return false }; // If not two element id's then abandon. 

        var className = params.class || 'muConnector'

        var lineStyle = params.lineStyle || '1px solid #666666';   // CSS style for connector line.

        this.gapX1 = params.gapX1 || (params.defaultGap ? params.defaultGap : 0);  // First element gap before start of connector, etc
        this.gapY1 = params.gapY1 || (params.defaultGap ? params.defaultGap : 0);
        this.gapX2 = params.gapX2 || (params.defaultGap ? params.defaultGap : 0);
        this.gapY2 = params.gapY2 || (params.defaultGap ? params.defaultGap : 0);


        this.gap = params.gap || 0; // use a single gap setting.
        if (this.gap > 0) {
            this.gapX1 = this.gap;
            this.gapY1 = this.gap;
            this.gapX2 = this.gap;
            this.gapY2 = this.gap;
        }

        var pos = function () { // only used for standalone drag processing.
            this.left = 0;
            this.top = 0;
        }

        this.PseudoGuid = new (function () { // Make a GUID to use in unique id assignment - from and credit to http://stackoverflow.com/questions/226689/unique-element-id-even-if-element-doesnt-have-one
            this.empty = "00000000-0000-0000-0000-000000000000";
            this.GetNew = function () {
                var fC = function () { return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1).toUpperCase(); }
                return (fC() + fC() + "-" + fC() + "-" + fC() + "-" + fC() + "-" + fC() + fC() + fC());
            };
        })();

        this.id = this.PseudoGuid.GetNew(); // use guid to avoid id-clashes with manual code.
        this.ele1 = document.getElementById(ele1);
        this.ele2 = document.getElementById(ele2);

        // Append the div that is the link line into the DOM
        this.lineID = 'L' + this.id;
        document.body.innerHTML += "<div id='" + this.lineID + "' class='" + className + "' style=  ></div>";
        this.line = document.getElementById('L' + this.id);

        this.line.style.position = 'absolute';
        this.line.style.borderLeft = lineStyle;
        this.line.style.zIndex = -100;

        // We may need to store the offsets of each element that we are connecting.
        this.offsets = [];
        this.offsets[ele1] = new pos;
        this.offsets[ele2] = new pos;

        this.link(); // show the initial link
    }

    link() {
        var linkEle1 = document.getElementById(this.ele1.id);
        var linkEle2 = document.getElementById(this.ele2.id);
        var line = document.getElementById('L' + this.id);

        var ele1rect = linkEle1.getBoundingClientRect();
        var originX = ele1rect.left + document.body.scrollLeft + linkEle1.offsetWidth / 2;
        var originY = window.pageYOffset + ele1rect.top + document.body.scrollTop + linkEle1.offsetHeight / 2;

        var ele2rect = linkEle2.getBoundingClientRect();
        var targetX = ele2rect.left + document.body.scrollLeft + linkEle2.offsetWidth / 2;
        var targetY = window.pageYOffset + ele2rect.top + document.body.scrollTop + linkEle2.offsetHeight / 2;

        var l = this.hyp((targetX - originX), (targetY - originY));
        var angle = 180 / 3.1415 * Math.acos((targetY - originY) / l);
        if (targetX > originX) { angle = angle * -1 }

        // Compute adjustments to edge of element plus gaps.
        var adj1 = this.edgeAdjust(angle, this.gapX1 + parseFloat(getComputedStyle(linkEle1, null).width.replace("px", "")) / 2, this.gapY1 + parseFloat(getComputedStyle(linkEle1, null).height.replace("px", "")) / 2)
        var adj2 = this.edgeAdjust(angle, this.gapX2 + parseFloat(getComputedStyle(linkEle2, null).width.replace("px", "")) / 2, this.gapY2 + parseFloat(getComputedStyle(linkEle2, null).height.replace("px", "")) / 2)

        l = l - (adj1.hp + adj2.hp)

        line.style.left = originX + 'px';
        line.style.height = l + 'px';
        line.style.width = 0;
        line.style.top = originY + adj1.hp + 'px';
        line.style.transform = 'rotate(' + angle + 'deg)';
        line.style.transformOrigin = '0 ' + (-1 * adj1.hp) + 'px';
    }

    Round(value, places) {
        var multiplier = Math.pow(10, places);
        return (Math.round(value * multiplier) / multiplier);
    }

    edgeAdjust(a, w1, h1) {
        var w = 0, h = 0

        // compute corner angles
        var ca = []
        ca[0] = Math.atan(w1 / h1) * 180 / 3.1415926 // RADIANS !!!
        ca[1] = 180 - ca[0]
        ca[2] = ca[0] + 180
        ca[3] = ca[1] + 180

        // Based on the possible sector and angle combinations work out the adjustments.
        if ((this.Round(a, 0) === 0)) {
            h = h1
            w = 0
        }
        else if ((this.Round(a, 0) === 180)) {
            h = h1
            w = 0
        }
        else if ((a > 0 && a <= ca[0]) || (a < 0 && a >= (-1 * ca[0]))) {
            h = h1
            w = -1 * Math.tan(a * (3.1415926 / 180)) * h1
        }

        else if (a > ca[0] && a <= 90) {
            h = Math.tan((90 - a) * (3.1415926 / 180)) * w1
            w = w1
        }
        else if (a > 90 && a <= ca[1]) {
            h = -1 * Math.tan((a - 90) * (3.1415926 / 180)) * w1
            w = w1
        }
        else if (a > ca[1] && a <= 180) {
            h = h1
            w = -1 * Math.tan((180 - a) * (3.1415926 / 180)) * h1
        }
        else if (a > -180 && a <= (-1 * ca[1])) {
            h = h1
            w = Math.tan((a - 180) * (3.1415926 / 180)) * h1
        }
        else if (a > (-1 * ca[1]) && a <= 0) {
            h = Math.tan((a - 90) * (3.1415926 / 180)) * w1
            w = w1
        }

        // We now have the width and height offsets - compute the hypotenuse.
        var hp = this.hyp(w, h)

        return { hp: hp }
    }

    hyp(X, Y) {
        return Math.abs(Math.sqrt((X * X) + (Y * Y)))
    }

    moved(e, ele) {
        var id = ele.attr('id');
        this.link()
    }

    Setup(connector, id) {
        var ele = $('#' + id);
        ele.on('mousedown.muConnector', function (e) {

            //#critical: tell the connector about the starting position when the mouse goes down.
            connector.offsets[id].left = e.pageX - ele.offset().left;
            connector.offsets[id].top = e.pageY - ele.offset().top;

            e.preventDefault();

            //hook the mouse move			
            ele.on('mousemove.muConnector', function (e) {
                ele.css({ left: e.pageX - connector.offsets[id].left, top: e.pageY - connector.offsets[id].top }); //element position = mouse - offset
                connector.moved(e, ele); // #critical: call the moved() function to update the connector position.
            });

            //define mouse up to cancel moving and mouse up, they are recreated each mousedown
            $(document).on('mouseup', function (e) {
                ele.off('mousemove.muConnector');
                //$(document).off('.muConnector');
            });
        });
    }
}

/*
    var connectors = [
      new muConnector(
          {
            ele1: "main",
            ele2: "red",
            lineStyle: "5px solid red",
            defaultGap: -35
        }
      ),
    ];
*/