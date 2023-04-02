interface JQuery {
    modal(settings: Settings): JQuery;
}

interface Settings {
    opacity?: number;
    inDuration?: number;
    outDuration?: number;
    onOpenStart?: Function;
    onOpenEnd?: Function;
    onCloseStart?: Function;
    onCloseEnd?: Function;
    dismissible?: boolean;
    startingTop?: string;
    endingTop?: string;
}