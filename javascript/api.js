var weixin = {
    appid: '',
    add: {
        timestamp: '',
        nonceStr: '',
    },
    pay: {
        backcode: '',
        timestamp: '',
        nonceStr: '',
    },
    js: {
        timestamp: '',
        nonceStr: '',
    },
    sign: '',
    scope: '',
    debug: false,
    zhuche: function (wf) {
        wx.config({
            debug: weixin.debug, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: weixin.appid, // 必填，公众号的唯一标识
            timestamp: weixin.js.timestamp, // 必填，生成签名的时间戳
            nonceStr: weixin.js.nonceStr, // 必填，生成签名的随机串
            signature: weixin.sign,// 必填，签名，见附录1
            jsApiList: wf // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });
    },
    fxweixinxiaoxi: function (title, desc, link, imgUrl, type, dataUrl, success, cancel) {
        var wf = new Array();
        wf.push('onMenuShareAppMessage');
        weixin.zhuche(wf);
        wx.ready(function () {
            wx.onMenuShareAppMessage({
                title: title, // 分享标题
                desc: desc, // 分享描述
                link: link, // 分享链接
                imgUrl: imgUrl, // 分享图标
                type: type, // 分享类型,music、video或link，不填默认为link
                dataUrl: dataUrl, // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    if (success) success();
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    if (cancel) cancel();
                    // 用户取消分享后执行的回调函数
                }
            });
        });
    },
    fxqq: function (title, desc, link, imgUrl, success, cancel) {
        wx.onMenuShareQQ({
            title: '', // 分享标题
            desc: '', // 分享描述
            link: '', // 分享链接
            imgUrl: '', // 分享图标
            success: function () {
                if (success) success();
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                if (cancel) cancel();
                // 用户取消分享后执行的回调函数
            }
        });
    },
    fxweibo: function (title, desc, link, imgUrl, success, cancel) {
        wx.onMenuShareWeibo({
            title: '', // 分享标题
            desc: '', // 分享描述
            link: '', // 分享链接
            imgUrl: '', // 分享图标
            success: function () {
                if (success) success();
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                if (cancel) cancel();
                // 用户取消分享后执行的回调函数
            }
        });
    },
    fxpengyouquan: function (title, link, imgUrl, success, cancel) {
        var wf = new Array();
        wf.push('onMenuShareTimeline');
        weixin.zhuche(wf);
        wx.ready(function () {
            wx.onMenuShareTimeline({
                title: title, // 分享标题
                link: link, // 分享链接
                imgUrl: imgUrl, // 分享图标
                success: function () {
                    if (success) success();
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    if (cancel) cancel();
                    // 用户取消分享后执行的回调函数
                }
            });
        });
    },
    openLocation: function (x, y, t, a) {
        var wf = new Array();
        wf.push('openLocation');
        weixin.zhuche(wf);

        wx.ready(function () {
            wx.openLocation({
                latitude: parseFloat(y), // 纬度，浮点数，范围为90 ~ -90
                longitude: parseFloat(x), // 经度，浮点数，范围为180 ~ -180。
                name: t, // 位置名
                address: a, // 地址详情说明
                scale: 15, // 地图缩放级别,整形值,范围从1~28。默认为最大
                infoUrl: '' // 在查看位置界面底部显示的超链接,可点击跳转
            });
        });
    },
    getLocation: function (call) {
        var wf = new Array();
        wf.push('getLocation');
        weixin.zhuche(wf);

        wx.ready(function () {
            wx.getLocation({
                type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                success: function (res) {
                    if (call) call(res);
                }
            });
        });
    },
    closewindow: function () {
        var wf = new Array();
        wf.push('closeWindow');
        weixin.zhuche(wf);

        wx.ready(function () {
            wx.closeWindow();
        });
    },
    xztp: function (call) {
        var wf = new Array();
        wf.push('chooseImage');
        weixin.zhuche(wf);
        wx.ready(function () {
            wx.chooseImage({
                success: function (res) {
                    if (call) call(res);
                }
            });
        });
    },
    yltp: function (current, urls) {
        var wf = new Array();
        wf.push('previewImage');
        weixin.zhuche(wf);
        wx.ready(function () {
            wx.previewImage({
                current: current, // 当前显示的图片链接
                urls: urls // 需要预览的图片链接列表
            });
        });
    },
    sctp: function (localId, isShowProgressTips, call) {
        var wf = new Array();
        wf.push('uploadImage');
        weixin.zhuche(wf);
        wx.ready(function () {
            wx.uploadImage({
                localId: localId, // 需要上传的图片的本地ID，由chooseImage接口获得
                isShowProgressTips: isShowProgressTips, // 默认为1，显示进度提示
                success: function (res) {
                    if (call) call(res);
                }
            });
        });
    },
    hqtp: function (success, mid) {
        $.ajax({
            type: "post",
            url: "Handler.ashx",
            data: "action=sctp&mid=" + mid,
            timeout: 60000,
            success: function (data) {
                if (success) success(data);
            },
            error: function (Request, textStatus) {
                alert('请求错误:' + textStatus);
            },
            dataType: "json"
        });
    }, jsApiAddrBack: function (o) {

    }, jsApiAddr: function () {
        try {
            WeixinJSBridge.invoke('editAddress', {
                "appId": weixin.appid,
                "scope": 'jsapi_address',
                "signType": "sha1",
                "addrSign": weixin.sign,
                "timeStamp": weixin.add.timestamp,
                "nonceStr": weixin.add.nonceStr
            },
            function (res) {
                if (res.err_msg == "edit_address:ok") {

                }
                if (res.userName != undefined) {
                    weixin.jsApiAddrBack(res);
                }
            }
            );
        } catch (e) {
            alert(e.message);
        }
    }, calladdr: function () {
        try {
            if (typeof WeixinJSBridge == "undefined") {
                if (document.addEventListener) {
                    document.addEventListener('WeixinJSBridgeReady', weixin.jsApiAddr, false);
                } else if (document.attachEvent) {
                    document.attachEvent('WeixinJSBridgeReady', weixin.jsApiAddr);
                    document.attachEvent('onWeixinJSBridgeReady', weixin.jsApiAddr);
                }
            } else {
                weixin.jsApiAddr();
            }
        } catch (e) {
            alert(e.message);
        }
    }, jsApiPayBackOk: function (o) {

    }, jsApiPayBackErro: function (o) {

    }, jsApiPay: function () {
        try {
            WeixinJSBridge.invoke(
                'getBrandWCPayRequest', {
                    "appId": weixin.appid, //公众号名称，由商户传入
                    "nonceStr": weixin.pay.nonceStr,//随机串
                    "package": weixin.package,
                    "signType": weixin.signType,//微信签名方式:
                    "timeStamp": weixin.pay.timestamp, //时间戳，自 1970 年以来的秒数
                    "paySign": weixin.paySign
                },
                function (res) {
                    if (res.err_msg == "get_brand_wcpay_request:ok") {
                        weixin.jsApiPayBackOk();
                    } else {
                        weixin.jsApiPayBackErro();
                    }
                }
            );
        } catch (e) {
            alert(e.message);
        }
    }, callpay: function () {
        try {
            if (typeof WeixinJSBridge == "undefined") {
                if (document.addEventListener) {
                    document.addEventListener('WeixinJSBridgeReady', weixin.jsApiPay, false);
                } else if (document.attachEvent) {
                    document.attachEvent('WeixinJSBridgeReady', weixin.jsApiPay);
                    document.attachEvent('onWeixinJSBridgeReady', weixin.jsApiPay);
                }
            } else {

                weixin.jsApiPay();
            }
        } catch (e) {
            alert(e.message);
        }
    },
    card: {
        shopId: '',
        cardType: '',
        cardId: '',
        timestamp: 0,
        nonceStr: '',
        signType: 'SHA1',
        cardSign: '',
        code: '',
        chooseBack: function (v) {

        },
        open: function () {
            var wf = new Array();
            wf.push('openCard');
            weixin.zhuche(wf);

            wx.ready(function () {
                wx.openCard({
                    cardList: [{
                        cardId: weixin.card.cardId,
                        code: weixin.card.code
                    }]// 需要打开的卡券列表
                });
            });

        },
        chooseCard: function () {
            var wf = new Array();
            wf.push('chooseCard');
            weixin.zhuche(wf);

            wx.ready(function () {
                wx.chooseCard({
                    shopId: weixin.card.shopId, // 门店Id
                    cardType: weixin.card.cardType, // 卡券类型
                    cardId: weixin.card.cardId, // 卡券Id
                    timestamp: weixin.card.timestamp, // 卡券签名时间戳
                    nonceStr: weixin.card.nonceStr, // 卡券签名随机串
                    signType: weixin.card.signType, // 签名方式，默认'SHA1'
                    cardSign: weixin.card.cardSign, // 卡券签名
                    success: function (res) {
                        var cardList = res.cardList; // 用户选中的卡券列表信息
                        weixin.card.chooseBack(cardList);
                    }
                });
            });

        }
    }
}