declare module '@deleteagency/dc' {
    class DcFactory {
        register(componentClass: any, selector?: string): void;

        init(root?: Document): void;
    }

    class DcBaseComponent {
        constructor(element: Element);

        element: Element;

        options: any;
    }

    const dcFactory: DcFactory;

    export { DcBaseComponent, dcFactory };
}
