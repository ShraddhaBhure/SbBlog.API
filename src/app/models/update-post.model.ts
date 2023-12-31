
export interface UpdatePostRequest{
   
    title: string | undefined;
    content : string | undefined;
    summary : string | undefined;
    urlHandel : string | undefined;
    featuredImageUrl : string | undefined;
    visible :boolean | undefined;
    author : string | undefined;
    publishDate: Date | undefined;
    updatedDate : Date | undefined;
}