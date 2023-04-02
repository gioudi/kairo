interface JQuery {
    bonsai(options: Options): JQuery;
    bonsai(action: string): void;
}

interface Options {
    expandAll?: boolean; // expand all items
    expand?: Function; // optional function to expand an item
    collapse?: Function; // optional function to collapse an item
    addExpandAll?: boolean; // add a link to expand all items
    addSelectAll?: boolean; // add a link to select all checkboxes
    selectAllExclude?: string; // a filter selector or function for selectAll
    idAttribute?: string; // which attribute of the list items to use as an id
    createInputs?: boolean;
    checkboxes?: boolean; //run qubit(this.options) on the root node(requires jquery.qubit)
    handleDuplicateCheckboxes?: boolean; // handleDuplicateCheckboxes: update any other checkboxes that
    // have the same value
}

