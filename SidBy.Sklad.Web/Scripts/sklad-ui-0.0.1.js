/// <reference path="~/Scripts/jquery-2.0.3.intellisense.js" />
/// <reference path="~/Scripts/jquery-ui-1.10.3.js" />
/// <reference path="~/Scripts/jquery.cookie.js" />

$(function () {
    // top navigation tabs
    $("#sklad-nav-tabs").tabs({
        active: $.cookie('activetab'),
        activate: function (event, ui) {
            $.cookie('activetab', ui.newTab.index(), {
                expires: 10
            });
        }
    });
});

            /*
            /// <summary>
            /// Оприходование
            /// </summary>
            Posting = 1,

            /// <summary>
            /// Списание
            /// </summary>
            Cancellation = 2,

            /// <summary>
            /// Заказы покупателей
            /// </summary>
            CustomerOrders = 3,

            /// <summary>
            /// Отгрузки
            /// </summary>
            Shipment = 4,

            /// <summary>
            /// Возвраты покупателей
            /// </summary>
            Refunds = 5
*/

function getDocumentTypeTitle(docTypeId)
{
    switch(docTypeId)
    {
        case 1:
            return "Оприходование";
            break;
        case 2:
            return "Списание";
            break;
        case 3:
            return "Заказ покупателя";
            break;
        case 4:
            return "Отгрузка";
            break;
        case 5:
            return "Возврат покупателя";
            break;
        default:
            return "Неизвестный тип доумента";
    }
}


// Hack. Use in conjunction with colHideJqGrid to display one to many
// relations
function colSpanAttrJqGrid(rowId, tv, rawObject, cm, rdata) {
    return ' colspan=2';
}

// Hack. Use in conjunction with colSpanJqGrid to display one to many
// relations
function colHideAttrJqGrid(rowId, tv, rawObject, cm, rdata) {
    return ' style="display:none;"';
}

function convertDateStringToJson(dateString) {
    if (dateString)
    {
        var splitChrs = dateString.split('-');
        if (splitChrs.length >= 2)
        {
            var dateObj = new Date(splitChrs[0] * 1, splitChrs[1] * 1-1, splitChrs[2] * 1);
            //console.log(dateObj);
            return dateObj.toJSONLocal();
        }
        // 2014-10-01
    }
 
    return null;
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

// Workaround: There is a bug with timezone offset when do Date.ToJson() function
Date.prototype.toJSONLocal = (function () {
    function addZ(n) {
        return (n < 10 ? '0' : '') + n;
    }
    return function () {
        return this.getFullYear() + '-' +
            addZ(this.getMonth() + 1) + "-" +
            addZ(this.getDate());
    };
}());

