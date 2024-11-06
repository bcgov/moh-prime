namespace Prime.Models
{
    public enum SiteStatusType
    {
        Editable = 1,
        InReview = 2,
        Locked = 3,

        //the status types below is used for filtering
        EditableNotApproved = 4,
        Flagged = 5,

        Archived = 8,
    }
}
