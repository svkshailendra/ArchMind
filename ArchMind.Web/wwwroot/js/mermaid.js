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

        const result = await mermaid.render(id, diagram);

        element.innerHTML = result.svg;
    }
    catch (error) {
        console.error("Mermaid rendering failed:", error);

        element.innerHTML = `
            <div class="alert alert-warning">
                Unable to render the architecture diagram.
            </div>`;
    }
};
