export interface Post{
    id: string;
    title: string;
    content : string;
    summary : string;
    urlHandel : string;
    featuredImageUrl : string;
    visible :boolean
    author : string;
    publishDate: Date;
    updatedDate : Date;
}