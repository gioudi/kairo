
declare class MStepper {
    constructor(stepper: HTMLElement, options?: Options);

    nextStep(cb: any): void;
    prevStep(cb: any): void;
    openStep(index: number, cb: any): void;
    updateStepper(): void;
    resetStepper(): void;
    wrongStep(): void;
}

interface Options {
    firstActive?: number;
    linearStepsNavigation?: boolean;
    autoFocusInput?: boolean;
    showFeedbackPreloader?: boolean;
    autoFormCreation?: boolean;
    validationFunction?: Function;
    stepTitleNavigation?: boolean;
    feedbackPreloader?: string;
}
