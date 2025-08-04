using TABP.Application.Common;
namespace TABP.Application.Reviews.Common
{
    public static class ReviewErrors
    {
        public static readonly Error ReviewNotFound = new(
            Code: "Review.NotFound",
            Description: "Review with this ID does not exist."
        );
        public static readonly Error ReviewAlreadyExists = new(
            Code: "Review.AlreadyExists",
            Description: "Review with this name already exists."
        );
        public static readonly Error InvalidReviewData = new(
            Code: "Review.InvalidData",
            Description: "The provided Review data is invalid."
        );
        public static readonly Error NotModified = new(
            Code: "Review.NotModified",
            Description: "The Review was not modified."
        );
        public static readonly Error ReviewUpdateFailed = new(
            Code: "Review.UpdateFailed",
            Description: "Failed to update the review. No records were modified."
        );
    }
}