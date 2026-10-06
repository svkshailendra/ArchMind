window.renderMermaid = async function (elementId, diagram) {

    if (!window.mermaid) {
        console.error("Mermaid library is not loaded.");
        throw new Error("Mermaid library is not loaded.");
    }

    const element =
        document.getElementById(elementId);

    if (!element) {
        console.error(
            "Mermaid container not found:",
            elementId);

        throw new Error(
            "Mermaid container not found: " + elementId);
    }

    if (!diagram || !diagram.trim()) {
        throw new Error(
            "Mermaid diagram is empty.");
    }

    try {

        const id =
            "mermaid-" +
            Date.now() +
            "-" +
            Math.random()
                .toString(36)
                .substring(2, 9);

        const cleanDiagram = diagram
            .replace(/\\n/g, "\n")
            .replace(/\r\n/g, "\n")
            .trim();

        console.log(
            "[ArchMind] Rendering Mermaid:",
            cleanDiagram);

        const result =
            await mermaid.render(
                id,
                cleanDiagram);

        element.innerHTML = result.svg;

        if (result.bindFunctions) {
            result.bindFunctions(element);
        }

        console.log(
            "[ArchMind] Mermaid rendered successfully.");
    }
    catch (error) {

        console.error(
            "[ArchMind] Mermaid rendering failed:",
            error
        );

        console.error(
            "[ArchMind] Mermaid source:",
            diagram
        );

        element.innerHTML = `
        <div class="alert alert-warning">
            <strong>Diagram could not be rendered.</strong>
            <div class="small">
                The architecture was generated successfully,
                but the AI-generated diagram contained invalid Mermaid syntax.
            </div>
        </div>
    `;

        return;
    }

};
