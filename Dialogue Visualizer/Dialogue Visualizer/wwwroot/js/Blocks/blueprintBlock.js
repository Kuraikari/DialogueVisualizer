function AddNewBlock(block) {
  var dimensions = {
    h: (block.height > 0) ? block.height : 150,
    w: (block.width > 0) ? block.width : 250
  }

  var position = {
    x: block.x,
    y: block.y
  };

  var style = [
    {
      property: `border-color`,
      value: `${block.color}`
    },
    {
      property: `background-color`,
      value: `${block.color}`
    },
  ];

  const idPrefix = 'dialogue-block';
  var elId = `${idPrefix}-${block.id}`;
  var builder = new HTMLElementBuilder(elId, style, dimensions, position);
  builder.text = `${block.dialogue.speaker} : ${block.dialogue.text}`;

  var element = builder.CreateElement().GetElement();
  document.querySelector('#canvas-outer').appendChild(element);
}

function GetBlocks() {
  $.get("@Url.Action("GetDialogueBlocks", "Home", new {projectId = Model.SelectedProjectId + 1})", function (scenes) {
    $.each(scenes, function (indexScene, scene) {
      $.each(scene.dialogueBlocks, function (index, block) {
        AddNewBlock(block);
        const prop = `${block.x}-${block.y}`
        diaBlocks[prop] = block;

        const idPrefix = 'dialogue-block';
        var elId = `${idPrefix}-${block.id}`;
        var elId2 = `${idPrefix}-${(block.dialogue.id - 1)}`;

        if (Object.keys(diaBlocks).length > 1) {

          var connector = new muConnector({
            ele1: `${elId2}`,
            ele2: `${elId}`,
            lineStyle: "4px solid black",
            defaultGap: -35
          });

          connectors.push(connector);

          var diaKeys = Object.keys(diaBlocks);
          diaKeys.forEach(key => {
            var elementId = `${idPrefix}-${diaBlocks[key].id}`;
            // dragElement(document.getElementById(elementId));
            contextMenu(elementId);
          });

          connectors.forEach(conn => {
            conn.Setup(conn, conn.ele1.id);
            conn.Setup(conn, conn.ele2.id);
          });
        }
      });
    });
  });
}

export default GetBlocks;