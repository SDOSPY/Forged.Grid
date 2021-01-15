/*!
 * Forged.Grid 1.0.0
 * https://github.com/SDOSPY/Forged.Grid
 *
 * Copyright © SDOSPY
 *
 * Licensed under the terms of the GNU GPL License
 * http://www.gnu.org/licenses/gpl-3.0.txt
 */

export interface ForgedGridOptions {
    url: URL;
    id: string;
    query: string;
    isAjax: boolean;
    loadingDelay: number | null;
    filters: {
        [type: string]: typeof ForgedGridFilter | undefined;
    };
}
export interface ForgedGridLanguage {
    [type: string]: {
        [method: string]: string;
    } | undefined;
}
export interface ForgedGridConfiguration {
    name: string;
    columns: {
        name: string;
        hidden: boolean;
    }[];
}
export declare class ForgedGrid {
    private static instances;
    static lang: ForgedGridLanguage;
    element: HTMLElement;
    columns: ForgedGridColumn[];
    pager?: ForgedGridPager;
    loader?: HTMLDivElement;
    controller: AbortController;
    url: URL;
    name: string;
    prefix: string;
    isAjax: boolean;
    loadingTimerId: number;
    loadingDelay: number | null;
    sort: Map<string, "asc" | "desc">;
    filterMode: "row" | "excel" | "header";
    filters: {
        [type: string]: typeof ForgedGridFilter | undefined;
    };
    constructor(container: HTMLElement, options?: Partial<ForgedGridOptions>);
    set(options: Partial<ForgedGridOptions>): this;
    showConfiguration(anchor?: HTMLElement): void;
    getConfiguration(): ForgedGridConfiguration;
    configure(configuration: ForgedGridConfiguration): void;
    reload(): void;
    private buildSort;
    private findGrid;
    private cleanUp;
    private bind;
}
export declare class ForgedGridColumn {
    name: string;
    grid: ForgedGrid;
    isHidden: boolean;
    header: HTMLElement;
    sort: ForgedGridColumnSort | null;
    filter: ForgedGridColumnFilter | null;
    constructor(grid: ForgedGrid, header: HTMLElement, rowFilter: HTMLElement | null);
    private cleanUp;
}
export declare class ForgedGridColumnSort {
    column: ForgedGridColumn;
    button: HTMLButtonElement;
    first: "asc" | "desc";
    order: "asc" | "desc" | "";
    constructor(column: ForgedGridColumn);
    toggle(multi: boolean): void;
    private bind;
}
export declare class ForgedGridColumnFilter {
    name: string;
    isApplied: boolean;
    defaultMethod: string;
    type: "single" | "double" | "multi";
    first: {
        method: string;
        values: string[];
    };
    operator: string;
    second: {
        method: string;
        values: string[];
    };
    column: ForgedGridColumn;
    instance?: ForgedGridFilter;
    button: HTMLButtonElement;
    rowFilter: HTMLElement | null;
    options: HTMLSelectElement | null;
    inlineInput: HTMLInputElement | null;
    constructor(column: ForgedGridColumn, rowFilter: HTMLElement | null);
    apply(): void;
    cancel(): void;
    private bind;
}
export declare class ForgedGridPager {
    grid: ForgedGrid;
    totalRows: number;
    currentPage: number;
    element: HTMLElement;
    showPageSizes: boolean;
    rowsPerPage: HTMLInputElement;
    pages: NodeListOf<HTMLElement>;
    constructor(grid: ForgedGrid, element: HTMLElement);
    apply(page: string): void;
    private cleanUp;
    private bind;
}
export declare class ForgedGridPopup {
    static draggedElement: HTMLElement | null;
    static draggedColumn: ForgedGridColumn | null;
    static lastActiveElement: HTMLElement | null;
    static element: HTMLDivElement;
    static showConfiguration(grid: ForgedGrid, anchor?: HTMLElement): void;
    static show(filter: ForgedGridColumnFilter): void;
    static hide(e?: UIEvent): void;
    private static setValues;
    private static setValue;
    private static createPreference;
    private static createDropzone;
    private static reposition;
    private static bind;
}
export declare class ForgedGridFilter {
    methods: string[];
    cssClasses: string;
    column: ForgedGridColumn;
    mode: "row" | "excel" | "header";
    type: "single" | "double" | "multi";
    constructor(column: ForgedGridColumn);
    init(): void;
    isValid(value: string): boolean;
    validate(input: HTMLInputElement): void;
    render(): string;
    renderFilter(name: "first" | "second"): string;
    renderOperator(): string;
    renderActions(): string;
    bindOperator(): void;
    bindMethods(): void;
    bindValues(): void;
    bindActions(): void;
}
export declare class ForgedGridTextFilter extends ForgedGridFilter {
    constructor(column: ForgedGridColumn);
}
export declare class ForgedGridNumberFilter extends ForgedGridFilter {
    constructor(column: ForgedGridColumn);
    isValid(value: string): boolean;
}
export declare class ForgedGridDateFilter extends ForgedGridFilter {
    constructor(column: ForgedGridColumn);
}
export declare class ForgedGridGuidFilter extends ForgedGridFilter {
    constructor(column: ForgedGridColumn);
    isValid(value: string): boolean;
}
