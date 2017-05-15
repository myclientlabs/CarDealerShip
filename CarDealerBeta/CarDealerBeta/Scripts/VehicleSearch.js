$(document).ready(function () {
    $("#btnSearch").on("click", function () {

        var isNewVehicleValue = $("#hdnIsNewVehicle").val();

        var data = {};
        if (isNewVehicleValue == null || isNewVehicleValue == "") {
            data.IsNewVehicle = null;
        }
        else {
            data.IsNewVehicle = isNewVehicleValue;
        }

        data.SearchText = $("#txtSearchText").val();
        data.MinPrice = $("#ddlMinPrice").val() == -1 ? null : $("#ddlMinPrice").val();
        data.MaxPrice = $("#ddlMaxPrice").val() == -1 ? null : $("#ddlMaxPrice").val();
        data.MinYear = $("#ddlMinYear").val() == -1 ? null : $("#ddlMinYear").val();
        data.MaxYear = $("#ddlMaxYear").val() == -1 ? null : $("#ddlMaxYear").val();

        $("#noResultMessage").hide();

        $.ajax({
            type: "POST",
            url: "/Inventory/SearchVehicles",
            data: data,
            success: function (result) {
                if (result.length > 0) {
                    $("#noResultMessage").hide();
                }
                else {
                    $("#noResultMessage").show();
                }
                $("#divSearchResult").html("");
                for (var i = 0; i < result.length; i++) {
                    var div = $("#VehicleTemplate").clone();
                    $(div).show();
                    $(div).find("#vhHeading").text(result[i].Year + " " + result[i].MakeName + " " + result[i].ModelName);
                    $(div).find("#vhBodyStyle").text(result[i].BodyStyleText);
                    $(div).find("#vhExteriorColor").text(result[i].ExteriorColor);
                    $(div).find("#vhInteriorColor").text(result[i].InteriorColor);
                    $(div).find("#vhTransmissionText").text(result[i].TransmissionText);

                    $(div).find("#vhMileage").text(result[i].Mileage);
                    $(div).find("#vhVin").text(result[i].Vin);
                    $(div).find("#vhMSRP").text("$" + result[i].MSRP);
                    $(div).find("#vhImage").attr("src", "/Images/" + result[i].ImageFile);

                    $(div).find("#vhSalesPrice").text("$" + result[i].SalesPrice);
                    $(div).find("#btnAction").attr("href", "/Inventory/Details/" + result[i].Id);

                    var viewRole = $("#hdnViewRole").val();
                    if (viewRole == "Admin") {
                        $(div).find("#btnAction").text("Edit");
                        $(div).find("#btnAction").attr("href", "/Admin/EditVehicle/" + result[i].Id);
                    }
                    if (viewRole == "Sales") {
                        $(div).find("#btnAction").text("Purchase");
                        $(div).find("#btnAction").attr("href", "/Sales/Purchase/" + result[i].Id);
                    }

                    $("#divSearchResult").append(div);
                }
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
    });
});
