import { Review } from "@/models/models";

interface CommentProps {
  review: Review;
}

const ReviewComponent: React.FC<CommentProps> = (props) => {
  const formatDate = (dateInput: Date): string => {
    const date = new Date(dateInput);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date.getDate().toString().padStart(2, "0");
    const hours = date.getHours().toString().padStart(2, "0");
    const minutes = date.getMinutes().toString().padStart(2, "0");

    return `${year}-${month}-${day} ${hours}:${minutes}`;
  };

  const formatModified = (dateInput: Date): string => {
    const date = new Date(dateInput);
    if (date.getDate() === new Date().getDate()) {
      return (
        date.getHours().toString().padStart(2, "0") +
        ":" +
        date.getMinutes().toString().padStart(2, "0")
      );
    }
    return formatDate(date);
  };  

  return (
    <div className="bg-orange-200 rounded-xl px-4 py-2 w-full">
      <div>
        <h3 className="text-orange-500 mb-2">{props.review.userEmail}</h3>
        <p>{props.review.text}</p>
        <p className="text-sm text-gray-500 font-bold">
          {formatDate(props.review.createdDate)}{" "}
          <span className="font-light">
            {props.review.modifiedDate
              ? `Modified (${formatModified(props.review.modifiedDate)})`
              : null}
          </span>
        </p>
      </div>
    </div>
  );
};

export default ReviewComponent;
