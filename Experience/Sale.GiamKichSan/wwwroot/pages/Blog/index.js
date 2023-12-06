$(document).ready(function () {
    index.init();
});

let index = {
    init: function () {
        index.generateMenu();
        $('.detail-categories-giamkichsan').empty();
    },
    generateMenu: function () {
        categoryRepository.getAll().done(data => {
            let $sectionUl = $('li.categories-giamkichsan>ul>li>div>div');
            let $sectionA = $('li.categories-giamkichsan>a');
            let IdRoot = 0;
            $sectionUl.empty();
            if (data == null || data.listObj == null || data.listObj.length == 0)
                return;

            for (let i = 0; i < data.listObj.length; i++) {
                if (data.listObj[i].idParent == 0) {
                    $sectionA.html(data.listObj[i].name);
                    IdRoot = data.listObj[i].id;
                }
            }

            for (let i = 0; i < data.listObj.length; i++) {
                if (data.listObj[i].idParent == IdRoot) {
                    $sectionUl.append('<div class="col-xs-12 col-sm-6 col-md-2 col-menu">' +
                        '<h2 class="title">' + data.listObj[i].name + '<ul class="links">' +
                        index.generateMenuLi(data.listObj, data.listObj[i].id) +
                        '</ul></h2>' +
                        '</div> ');
                }
            }
        });
    },
    generateMenuLi: function (data, idParent) {
        let strLi = '';
        for (let i = 0; i < data.length; i++) {
            if (data[i].idParent == idParent) {
                strLi += '<li data-key="' + data[i].id + '"><a href="#" onclick="index.generateContent(' + data[i].id + ')">' + data[i].name + '</a></li>';
            }
        }
        return strLi;
    },
    generateContent: function (idCategory) {
        var $detailCategories = $('.detail-categories-giamkichsan');
        $detailCategories.empty();

        blogRepository.getAllByIDCatetory(idCategory).done(data => {
            for (let i = 0; i < data.listObj.length; i++) {
                var firstClass = '';
                if (i > 0)
                    firstClass = 'outer-top-bd';
                $detailCategories.append('<div class="blog-post ' + firstClass + '  wow fadeInUp animated" style="visibility: visible; animation-name: fadeInUp;">' +
                    '<a href="' + data.listObj[i].imageAvatar + '"><img class="img-responsive" src="' + data.listObj[i].imageAvatar + '" alt=""></a>' +
                    '<h1><a href="#">' + data.listObj[i].title + '</a></h1>' +
                    '<span class="author">Công Duân</span>' +
                    '<span class="review">6 Comments</span>' +
                    '<span class="date-time">' + data.listObj[i].dateShow + '</span>' +
                    '<p>' + data.listObj[i].description + '</p><a onclick="index.showDetails(' + data.listObj[i].id + ')" class="btn btn-upper btn-primary read-more">read more</a>' +
                    '</div>');
            }
        });
    },
    showDetails: function (idBlog) {
        let $model = $('.content-blog-giamkichsan');
        $model.html('');
        $model = $('<div class="container"></div>').appendTo($model);

        blogRepository.getByID(idBlog).done((data) => {
            if (data != null && data.obj != null) {
                if (data.obj.blog != null) {
                    $model.append('<img class="img-responsive" src="' + data.obj.blog.imageAvatar + '" alt="' + data.obj.blog.title + '">');
                    $model.append('<h1>' + data.obj.blog.title + '</h1>');
                    $model.append('<span class="author">Công Duân</span>');
                    $model.append('<span class="review">7 Comments</span>');
                    $model.append('<span class="date-time">' + data.obj.blog.dateShow + '</span>');
                    $model.append('<p>' + data.obj.blog.description + '</p>');
                    $model.append('</br>');
                }
                if (data.obj.blogDetails != null && data.obj.blogDetails.length > 0) {
                    let details = '';
                    data.obj.blogDetails.forEach(element => { details += element.description; });
                    $model.append(details);
                }
                document.getElementById('ModalButton').setAttribute('data-target', '#ModalCreate');
                document.getElementById('ModalButton').click();
            }
        });
    }
};