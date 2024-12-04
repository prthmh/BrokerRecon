
const serviceUrl = "http://localhost:54763/"

function fileAdaptorValueChange(e, modelValue) {
    
    console.log(e.value, modelValue);

    if (modelValue === "Source") {
        sourceAdaptor(e);
    } else {
        targetAdaptor(e);
    }
}

function sourceAdaptor(e) {
    let $fileInput = $("#file_input");

    clearOldSourceUI($fileInput);

    if (e.value === "Excel File") {
        $fileInput.dxFileUploader({
            allowedFileExtensions: ['.xlsx', '.xls'],
            selectButtonText: "Select Excel File",
            accept: ".xlsx,.xls",
            labelMode: "static",
            label: "Excel File",
            styleMode: "filled",    
            width: "100%",
            height: "100%",
            uploadUrl: '@Url.Action("Info", "UploadSourceFile")',
            uploadMode: "useForm",
            name: "file",
            elementAttr: {
                class: "file-uploader-box"
            }
        });
    } else {
        console.log("inside select")
        $fileInput.dxSelectBox({
            dataSource: DevExpress.data.AspNet.createStore({
                key: "id",
                loadUrl: serviceUrl + "/City/GetAllCitites",
            }),
            displayExpr: "city",
            searchEnabled: true,
            label: "ER Report",
            labelMode: "static",
            //height: "58px"
        });
    }
}

function clearOldSourceUI($fileInput) {
    if ($fileInput.data("dxSelectBox")) {
        $fileInput.dxSelectBox("dispose");
    }
    if ($fileInput.data("dxFileUploader")) {
        $fileInput.dxFileUploader("dispose");
    }
}

function targetAdaptor(e) {
    let $targetFileInput = $("#file_input_target");
    //$("#file_input_target").empty();

    clearOldTargetUI($targetFileInput);

    if (e.value === "Excel File") {
        $targetFileInput.dxFileUploader({
            allowedFileExtensions: ['.xlsx', '.xls'],
            selectButtonText: "Select Excel File",
            //labelText: "or Drop Excel file here",
            accept: ".xlsx,.xls",
            labelMode: "static",
            label: "Excel File",
            styleMode: "filled",
            width: "100%",
            height: "100%",
            uploadUrl: '@Url.Action("Info", "UploadTargetFile")',
            uploadMode: "useForm",
            name: "target_file",
            elementAttr: {
                class: "file-uploader-box"
            }
        });
    } else {
        $targetFileInput.dxSelectBox({
            dataSource: DevExpress.data.AspNet.createStore({
                key: "id",
                loadUrl: serviceUrl + "/City/GetAllCitites",
            }),
            displayExpr: "city",
            searchEnabled: true,
            label: "ER Report",
            labelMode: "static",
            //height: "58px"
        });
    }

}

function clearOldTargetUI($targetFileInput) {

    if ($targetFileInput.data("dxSelectBox")) {
        $targetFileInput.dxSelectBox("dispose");
    }

    if ($targetFileInput.data("dxFileUploader")) {
        $targetFileInput.dxFileUploader("dispose")
    }
}

function onSaveClick() {
    const sourceDetailDataGrid = $("#sourceGridContainer").dxDataGrid("instance");
    const targetDetailDataGrid = $("#targetGridContainer").dxDataGrid("instance");

    const codeComp = $("#code").dxTextBox("instance");
    const nameComp = $("#name").dxTextBox("instance");
    const descComp = $("#description").dxTextArea("instance");
    const matchComp = $("#match").dxColorBox("instance");
    const warningComp = $("#warning").dxColorBox("instance");
    const mismatchComp = $("#mismatch").dxColorBox("instance");
    const sourceFileAdaptorComp = $("#Source").dxSelectBox("instance");
    const sourceERReportComp = $("#file_input").dxSelectBox()
        ? $("#file_input").dxSelectBox("instance") : null;
    const sourceExcelFileComp = $("#file_input").dxFileUploader()
        ? $("#file_input").dxFileUploader("instance") : null;
    const targetFileAdaptorComp = $("#Target").dxSelectBox("instance");
    const targetERReportComp = $("#file_input_target").dxSelectBox()
        ? $("#file_input_target").dxSelectBox("instance") : null;
    const targetExcelFileComp = $("#file_input_target").dxFileUploader()
        ? $("#file_input_target").dxFileUploader("instance") : null;

    const infoObj = {
        Code: codeComp.option("value").toUpperCase(),
        Name: nameComp.option("value"),
        Description: descComp.option("value"),
        Match: matchComp.option("value"),
        Warning: warningComp.option("value"),
        MisMatch: mismatchComp.option("value"),
        Files: [
            {
                Indicator: "Source",
                FileAdaptorType: sourceFileAdaptorComp.option("value"),
                ERReport: sourceERReportComp ? sourceERReportComp.option("value")?.city : null
            },
            {
                Indicator: "Target",
                FileAdaptorType: targetFileAdaptorComp.option("value"),
                ERReport: targetERReportComp ? targetERReportComp.option("value")?.city : null
            }
        ]
    };

    
    const sourceFormData = new FormData();

    if (sourceExcelFileComp && sourceExcelFileComp.option("value").length > 0) {

        sourceFormData.append("sourceExcelFile", sourceExcelFileComp.option("value")[0]);

        $.ajax({
            url: serviceUrl +  "/Info/UploadSourceFile",
            type: "POST",
            data: sourceFormData,
            contentType: false,  
            processData: false,  
            success: function (response) {
                if (response.success) {
                    sourceDetailDataGrid.refresh();
                    
                } else {
                    $("#uploadStatus").html('<span style="color:red;">' + response.message + '</span>');
                }
            },
            error: function () {
                $("#uploadStatus").html('<span style="color:red;">Error uploading file.</span>');
            }
        });
    }

    const targetFormData = new FormData();
    if (targetExcelFileComp && targetExcelFileComp.option("value").length > 0) {

        targetFormData.append("targetExcelFile", targetExcelFileComp.option("value")[0]);

        $.ajax({
            url: serviceUrl + "/Info/UploadTargetFile",
            type: "POST",
            data: targetFormData,
            contentType: false,  
            processData: false,  
            success: function (response) {
                if (response.success) {
                    targetDetailDataGrid.refresh();

                } else {
                    $("#uploadStatus").html('<span style="color:red;">' + response.message + '</span>');
                }
            },
            error: function () {
                $("#uploadStatus").html('<span style="color:red;">Error uploading file.</span>');
            }
        });
    }


    console.log(infoObj);
    console.log(JSON.stringify(infoObj))
    
    $.ajax({
        url: serviceUrl + "Info/Insert",
        type: "POST",
        data: JSON.stringify(infoObj),
        contentType: "application/json",
        //contentType: false, 
        //processData: false, 
        success: function (response) {
            if (response.success) {
                DevExpress.ui.notify("Data saved successfully", "success", 3000);
                codeComp.option("value", "");
                nameComp.option("value", "");
                descComp.option("value", "");
                matchComp.option("value", "");
                warningComp.option("value", "");
                mismatchComp.option("value", "");

                let $fileInput = $("#file_input");
                sourceFileAdaptorComp.reset();
                clearOldSourceUI($fileInput);

                let $targetFileInput = $("#file_input_target");
                targetFileAdaptorComp.reset();
                clearOldTargetUI($targetFileInput);
                $("#file_input").dxTextBox();
                $("#file_input_target").dxTextBox();
            } else {
                DevExpress.ui.notify("Failure", "error", 3000);
            }
        },
        error: function (error) {
            DevExpress.ui.notify("Error saving users!", "error", 3000);
        }
    });
}




function calSourceCell(rowData) {
    const sourceFile = rowData.Files[0];
    console.log(rowData.Files)
    return sourceFile
        ? `${sourceFile.FileAdaptorType} - ${sourceFile.ERReport || sourceFile.ExcelFile}`
        : '';
}
function calTargetCell(rowData) {
    const sourceFile = rowData.Files[1];
    return sourceFile
        ? `${sourceFile.FileAdaptorType} - ${sourceFile.ERReport || sourceFile.ExcelFile}`
        : '';
}

function cellTemplate(container, options) {
    const value = options.value;
    console.log(options);
    container.html(`<div style="background-color: ${value}; width: 20px; height: 20px; border: 1px solid #ddd; display: inline-block; vertical-align: middle; margin-right: 5px;"></div>${value}`);
}

function targetCellTemplate(container, options) {
    console.log("options", options);
    if (options.modified) {
        container.html(`<div>${options.value}</div>`);
    } else {
        container.html(`<div></div>`);
    }
}

function onEditorPrepared(e) {
    console.log("oneditor", e)
    if (e.dataField === "ColumnName" && e.row) {
        $(e.editorElement).on('change', function () {
            const selectedValue = $(this).val();
            console.log("selected value", selectedValue);

           
        });
    }
}


function editCellTemplate(cellElement, cellInfo) {
    $("<div>").dxSelectBox({
        dataSource: DevExpress.data.AspNet.createStore({
            key: "ID",
            loadUrl: serviceUrl + "Info/GetTargetTable",
        }),
        displayExpr: "ColumnName",
        valueExpr: "ColumnName",
        onValueChanged: function (e) {
            onTargetCellValueChange(e, cellInfo);
        }
    }).appendTo(cellElement);
}

function onTargetCellValueChange(e, cellInfo) {

    console.log("Selected Value:", e.value);

    var dataGridInstance = $("#targetGridContainer").dxDataGrid("instance");
    console.log("getVisibleColumns", dataGridInstance.getVisibleColumns());
    console.log("getVisibleRows", dataGridInstance.getVisibleRows());
    const targetTableData = dataGridInstance.getVisibleRows().map(r => r.data);
    var selectedItem = targetTableData.find(item => item.ColumnName === e.value);
    console.log("targetData", targetTableData);
    console.log("Selected Item:", selectedItem);

    if (selectedItem) {
        console.log(cellInfo.row.rowIndex, 3, selectedItem.Value)
        dataGridInstance.cellValue(cellInfo.row.rowIndex, 3, selectedItem.Value);
        dataGridInstance.cellValue(cellInfo.row.rowIndex, 1, typeof selectedItem.Value);
        cellInfo.setValue(e.value);
        dataGridInstance.cellValue(cellInfo.row.rowIndex, 0, e.value);
    }

    //DevExpress.data.AspNet.createStore({
    //    key: "ID",
    //    loadUrl: serviceUrl + "Info/GetTargetTable",
    //}).load().done(function (data) {
    //    var selectedItem = data.find(item => item.ColumnName === e.value);
    //    console.log("Selected Item:", selectedItem);

    //    if (selectedItem) {
    //        console.log(cellInfo.row.rowIndex, 3, selectedItem.Value)
    //        dataGridInstance.cellValue(cellInfo.row.rowIndex, 3, selectedItem.Value);
    //        dataGridInstance.cellValue(cellInfo.row.rowIndex, 1, typeof selectedItem.Value);
    //        cellInfo.setValue(e.value);
    //        dataGridInstance.cellValue(cellInfo.row.rowIndex, 0, e.value);
    //    }
    //});
}

 