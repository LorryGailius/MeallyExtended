import { Review } from "@/models/models";

interface CommentProps {
  review: Review;
}

const ReviewComponent: React.FC<CommentProps> = (props) => {
  const formatDate = (dateInput: Date): string => {
    const date = new Date(dateInput);
    return (
      date.getFullYear() +
      "-" +
      (date.getMonth() + 1) +
      "-" +
      date.getDate() +
      " " +
      date.getHours() +
      ":" +
      date.getMinutes()
    );
  };

  const formatModified = (dateInput: Date): string => {
    const date = new Date(dateInput);
    if (date.getDate() === new Date().getDate()) {
      formatDate(date);
    }
    return date.getHours() + ":" + date.getMinutes();
  };

  return (
    <div className="bg-orange-200 rounded-xl px-4 py-2 w-full">
      <div>
        <h3 className="text-orange-500 mb-2">{props.review.userEmail}</h3>
        <p>{props.review.text}</p>
        <p className="text-sm text-gray-500">
          {formatDate(props.review.createdDate)}{" "}
          {props.review.modifiedDate
            ? `Modified (${formatModified(props.review.modifiedDate)})`
            : null}
        </p>
      </div>
    </div>
  );
};

export default ReviewComponent;
