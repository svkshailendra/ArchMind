window.renderMermaid = async function (elementId, diagram) {
    if (!window.mermaid) {
        console.error("Mermaid library is not loaded.");
        return;
    }

    const element = document.getElementById(elementId);

    if (!element) {
        console.error("Mermaid container not found:", elementId);
        return;
    }

    try {
        const id = "mermaid-" + Date.now();

        const cleanDiagram = diagram.replace(/\\n/g, "\n");

        console.log("Mermaid diagram being rendered:", cleanDiagram);

        const result = await mermaid.render(id, cleanDiagram);

        element.innerHTML = result.svg;
    }
    catch (error) {
        console.error("Mermaid rendering failed:", error);
        console.error("Invalid Mermaid diagram:", diagram);


        element.innerHTML = `
            <div class="alert alert-warning">
                Unable to render the architecture diagram.
            </div>`;
    }
};
